#if IOS
using AuthenticationServices;
using Foundation;
using UIKit;

namespace EventosCorferias.Platforms.iOS
{
    public class AppleAuthPresentationProvider : NSObject, IASAuthorizationControllerPresentationContextProviding
    {
        public UIWindow GetPresentationAnchor(ASAuthorizationController controller)
        {
            return UIApplication.SharedApplication.KeyWindow ?? new UIWindow();
        }
    }
}
#endif
