#if IOS
using AuthenticationServices;
using EventosCorferias.AppleAuth;
using Foundation;

namespace EventosCorferias.Platforms.iOS
{
    public class AppleAuthControllerDelegate : ASAuthorizationControllerDelegate
    {
        private readonly Action<string, AppleSignInResult?> onResult;

        public AppleAuthControllerDelegate(Action<string, AppleSignInResult?> callback)
        {
            onResult = callback;
        }

        public override void DidComplete(ASAuthorizationController controller, ASAuthorization authorization)
        {
            if (authorization.GetCredential<ASAuthorizationAppleIdCredential>() is ASAuthorizationAppleIdCredential credential)
            {
                var result = new AppleSignInResult
                {
                    UserId = credential.User,
                    Email = credential.Email,
                    FullName = $"{credential.FullName?.GivenName} {credential.FullName?.FamilyName}"
                };

                onResult("Login exitoso", result);
            }
            else
            {
                onResult("⚠️ No se obtuvo un AppleIDCredential válido.", null);
            }
        }

        public override void DidComplete(ASAuthorizationController controller, NSError error)
        {
            onResult($"❌ Error: {error.LocalizedDescription}", null);
        }
    }


}
#endif
