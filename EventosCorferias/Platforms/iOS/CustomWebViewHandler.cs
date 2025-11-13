using EventosCorferias.Controls;
using Foundation;
using Microsoft.Maui.Handlers;
using WebKit;

namespace EventosCorferias.Platforms.iOS
{
    public class CustomWebViewHandler : ViewHandler<CustomWebView, WKWebView>
    {
        public static PropertyMapper<CustomWebView, CustomWebViewHandler> Mapper = new(ViewMapper)
        {
        };

        public CustomWebViewHandler() : base(Mapper) { }

        protected override WKWebView CreatePlatformView()
        {
            var config = new WKWebViewConfiguration
            {
                Preferences = new WKPreferences
                {
                    JavaScriptEnabled = true
                }
            };

            return new WKWebView(CoreGraphics.CGRect.Empty, config);
        }

        protected override void ConnectHandler(WKWebView platformView)
        {
            base.ConnectHandler(platformView);

            LoadUrlWithPostData(platformView);
        }

        private void LoadUrlWithPostData(WKWebView webView)
        {
            if (VirtualView is not CustomWebView customWebView ||
                customWebView.Pagos == null ||
                string.IsNullOrEmpty(customWebView.Url))
                return;

            string postData = $"ANO={customWebView.Pagos.ANO}&CHKTERMINO={customWebView.Pagos.CHKTERMINO}&EVENTO={customWebView.Pagos.EVENTO}" +
                              $"&TXTCANTIDAD={customWebView.Pagos.TXTCANTIDAD}&TXTDESCRIPCION={customWebView.Pagos.TXTDESCRIPCION}" +
                              $"&TXTEMAIL={customWebView.Pagos.TXTEMAIL}&TXTEMAIL_CONFIRMAR={customWebView.Pagos.TXTEMAIL_CONFIRMAR}" +
                              $"&TXTIDENTIFICACION={customWebView.Pagos.TXTIDENTIFICACION}&TXTMODULO={customWebView.Pagos.TXTMODULO}" +
                              $"&TXTIVA={customWebView.Pagos.TXTIVA}&TXTMONEDA={customWebView.Pagos.TXTMONEDA}&TXTMONTO={customWebView.Pagos.TXTMONTO}" +
                              $"&TXTNUMREFERENCIA={customWebView.Pagos.TXTNUMREFERENCIA}&TXTIDEEVE={customWebView.Pagos.TXTIDEEVE}" +
                              $"&TXTCATEG={customWebView.Pagos.TXTCATEG}&TXTNOMBRE={customWebView.Pagos.TXTNOMBRE}&TXTTELEFONO={customWebView.Pagos.TXTTELEFONO}" +
                              $"&TXTTIPFERIA={customWebView.Pagos.TXTTIPFERIA}&TXTMONIVA={customWebView.Pagos.TXTMONIVA}" +
                              $"&TXTEMPDIREC={customWebView.Pagos.TXTEMPDIREC}&TXTEMPPAIS={customWebView.Pagos.TXTEMPPAIS}" +
                              $"&TXTEMPDEPTO={customWebView.Pagos.TXTEMPDEPTO}&TXTEMPCIUDAD={customWebView.Pagos.TXTEMPCIUDAD}" +
                              $"&TXTEMPNOMBRE={customWebView.Pagos.TXTEMPNOMBRE}&TXTEMPTELEF={customWebView.Pagos.TXTEMPTELEF}" +
                              $"&TXTTOTMONT={customWebView.Pagos.TXTTOTMONT}&TXTAPELLIDO={customWebView.Pagos.TXTAPELLIDO}";

            var url = new NSUrl(customWebView.Url);
            var request = new NSMutableUrlRequest(url)
            {
                HttpMethod = "POST"
            };

            request.Body = NSData.FromString(postData, NSStringEncoding.UTF8);
            request["Content-Type"] = "application/x-www-form-urlencoded";

            webView.LoadRequest(request);
        }
    }
}
