using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Maui.Authentication;
using EventosCorferias.Models;
using System.Net.Http;

#if IOS
using Security;
using Foundation;
#endif

namespace EventosCorferias.GoogleAuth
{
    public partial class GoogleAuthService
    {
        // ✅ VALORES TOMADOS DE TU GoogleService-Info.plist (eventoess)

        private const string GoogleClientId = "438335159031-utddv5tac0cof6dsqr597gl0atagaaag.apps.googleusercontent.com";

        private const string RedirectUri = "com.googleusercontent.apps.438335159031-utddv5tac0cof6dsqr597gl0atagaaag:/oauth2redirect";

        private const string FirebaseApiKey = "AIzaSyDY7pSzPUl2w9ZfNYQbplz2t0ZjAvjlCjw";

        private readonly HttpClient _httpClient;

        public GoogleAuthService()
        {
#if IOS
            var sessionConfiguration = NSUrlSessionConfiguration.DefaultSessionConfiguration;
            sessionConfiguration.TlsMinimumSupportedProtocolVersion = TlsProtocolVersion.Tls12;

            var handler = new NSUrlSessionHandler(sessionConfiguration);
            _httpClient = new HttpClient(handler);
#else
            _httpClient = new HttpClient();
#endif
        }

        public async Task<GoogleUserDTO?> AuthenticateAsync()
        {
            ClaseBase log = new ClaseBase();

            try
            {
                string startMsg = "🚀 Iniciando Google Login (Auth Code Flow - .NET 10)";
                log.InsertarLogs_Mtd("INFO", startMsg, "GoogleAuth apple", "Start", "iOS");
                System.Diagnostics.Debug.WriteLine($"[GoogleAuth] INFO: {startMsg}");

                var nonce = Guid.NewGuid().ToString("N");

                var authUrl = new StringBuilder("https://accounts.google.com/o/oauth2/v2/auth")
                    .Append($"?client_id={GoogleClientId}")
                    .Append("&response_type=code")
                    .Append("&scope=openid%20email%20profile")
                    .Append($"&redirect_uri={Uri.EscapeDataString(RedirectUri)}")
                    .Append($"&nonce={nonce}")
                    .ToString();

                log.InsertarLogs_Mtd("INFO", "Abriendo navegador seguro...", "GoogleAuth apple", "AuthUrl", "iOS");

                var result = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(authUrl),
                    new Uri(RedirectUri)
                );

                if (!result.Properties.TryGetValue("code", out var authCode))
                {
                    string errorNoCode = "❌ No se recibió el 'code' de Google";
                    log.InsertarLogs_Mtd("ERROR", errorNoCode, "GoogleAuth apple", "AuthError", "iOS");
                    return null;
                }

                log.InsertarLogs_Mtd("INFO", "✅ Código recibido, intercambiando con Google...", "GoogleAuth apple", "AuthSuccess", "iOS");

                var tokenValues = new Dictionary<string, string>
                {
                    { "code", authCode },
                    { "client_id", GoogleClientId },
                    { "redirect_uri", RedirectUri },
                    { "grant_type", "authorization_code" }
                };

                var tokenContent = new FormUrlEncodedContent(tokenValues);
                var tokenResponse = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", tokenContent);

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    var errorContent = await tokenResponse.Content.ReadAsStringAsync();
                    log.InsertarLogs_Mtd("ERROR", $"Error obteniendo Token: {errorContent}", "GoogleAuth apple", "TokenError", "iOS");
                    return null;
                }

                var tokenJson = await tokenResponse.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<GoogleTokenResponse>(tokenJson);

                if (tokenData == null || string.IsNullOrEmpty(tokenData.access_token))
                {
                    log.InsertarLogs_Mtd("ERROR", "Token de acceso nulo o vacío", "GoogleAuth apple", "TokenError", "iOS");
                    return null;
                }

                var userInfoResponse = await _httpClient.GetAsync(
                    $"https://www.googleapis.com/oauth2/v2/userinfo?access_token={tokenData.access_token}");

                if (!userInfoResponse.IsSuccessStatusCode)
                {
                    var errorContent = await userInfoResponse.Content.ReadAsStringAsync();
                    log.InsertarLogs_Mtd("ERROR", $"Error obteniendo UserInfo: {errorContent}", "GoogleAuth apple", "UserInfoError", "iOS");
                    return null;
                }

                var userInfoJson = await userInfoResponse.Content.ReadAsStringAsync();
                var googleUser = JsonSerializer.Deserialize<GoogleUserInfo>(userInfoJson);

                if (googleUser == null)
                {
                    log.InsertarLogs_Mtd("ERROR", "Datos de usuario nulos", "GoogleAuth apple", "UserInfoError", "iOS");
                    return null;
                }

                log.InsertarLogs_Mtd("INFO", $"Usuario autenticado: {googleUser.email}", "GoogleAuth apple", "Success", "iOS");

                return new GoogleUserDTO
                {
                    Email = googleUser.email,
                    FullName = googleUser.name,
                    UserName = googleUser.email,
                    TokenId = tokenData.id_token
                };
            }
            catch (TaskCanceledException)
            {
                log.InsertarLogs_Mtd("INFO", "El usuario cerró el navegador", "GoogleAuth apple", "Cancel", "iOS");
                return null;
            }
            catch (Exception ex)
            {
                string criticalError = $"Error crítico: {ex.Message}";
                log.InsertarLogs_Mtd("ERROR", criticalError, "GoogleAuth apple", "Exception", "iOS");
                return null;
            }
        }

        public Task<GoogleUserDTO> GetCurrentUserAsync()
        {
            return Task.FromResult<GoogleUserDTO>(null);
        }

        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class GoogleTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string id_token { get; set; }
    }

    public class GoogleUserInfo
    {
        public string id { get; set; }
        public string email { get; set; }
        public bool verified_email { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string locale { get; set; }
    }
}