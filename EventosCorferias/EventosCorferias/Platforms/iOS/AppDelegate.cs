using EventosCorferias.Models;
using Foundation;
using UIKit;

namespace EventosCorferias
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            ClaseBase claseBase = new ClaseBase();
            Console.WriteLine("🔥 OPEN URL CALLBACK: " + url.AbsoluteString);
            return WebAuthenticator.Default.OpenUrl(app, url, options);
        }

    }
}
