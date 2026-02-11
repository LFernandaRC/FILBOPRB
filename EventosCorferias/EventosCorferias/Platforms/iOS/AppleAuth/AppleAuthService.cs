#if IOS
using AuthenticationServices;
using EventosCorferias.AppleAuth;
using Foundation;
using System.Threading.Tasks;
using UIKit;

namespace EventosCorferias.Platforms.iOS
{
    public class AppleAuthService : NSObject, IAppleSignInService, IASAuthorizationControllerDelegate, IASAuthorizationControllerPresentationContextProviding
    {
        private TaskCompletionSource<AppleSignInResult?>? tcs;
        private Action<string>? log;

        public Task<AppleSignInResult?> SignInAsync(Action<string>? onLog = null)
        {
            tcs = new TaskCompletionSource<AppleSignInResult?>();
            log = onLog;

            var provider = new ASAuthorizationAppleIdProvider();
            var request = provider.CreateRequest();
            request.RequestedScopes = new[] { ASAuthorizationScope.Email, ASAuthorizationScope.FullName };

            var controller = new ASAuthorizationController(new[] { request });
            controller.Delegate = this;
            controller.PresentationContextProvider = this;
            controller.PerformRequests();

            log?.Invoke("📤 Solicitud de inicio de sesión enviada...");

            return tcs.Task;
        }

        public void DidComplete(ASAuthorizationController controller, ASAuthorization authorization)
        {
            log?.Invoke("✅ Respuesta recibida...");

            if (authorization.GetCredential<ASAuthorizationAppleIdCredential>() is ASAuthorizationAppleIdCredential credential)
            {
                var result = new AppleSignInResult
                {
                    UserId = credential.User,
                    Email = credential.Email,
                    FullName = $"{credential.FullName?.GivenName} {credential.FullName?.FamilyName}"
                };

                log?.Invoke($"🎉 Login completado\nID: {result.UserId}\nEmail: {result.Email}\nNombre: {result.FullName}");
                tcs?.TrySetResult(result);
            }
            else
            {
                log?.Invoke("⚠️ No se obtuvo un AppleIDCredential válido.");
                tcs?.TrySetResult(null);
            }
        }

        public void DidComplete(ASAuthorizationController controller, NSError error)
        {
            log?.Invoke($"❌ Error: {error.LocalizedDescription}");
            tcs?.TrySetResult(null);
        }

        public UIWindow GetPresentationAnchor(ASAuthorizationController controller)
        {
            return UIApplication.SharedApplication.KeyWindow ?? new UIWindow();
        }
    }
}
#endif
