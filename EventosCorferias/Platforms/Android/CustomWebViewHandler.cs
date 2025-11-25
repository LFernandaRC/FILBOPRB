using Android.Webkit;
using EventosCorferias.Controls;
using Microsoft.Maui.Handlers;
using System.Text;
using WebView = Android.Webkit.WebView;

namespace EventosCorferias.Platforms.Android
{
    public class CustomWebViewHandler : ViewHandler<CustomWebView, WebView>
    {
        public static PropertyMapper<CustomWebView, CustomWebViewHandler> Mapper = new PropertyMapper<CustomWebView, CustomWebViewHandler>(ViewMapper)
        {
        };

        public CustomWebViewHandler() : base(Mapper) { }

        protected override WebView CreatePlatformView()
        {
            var webView = new WebView(Context);
            webView.Settings.JavaScriptEnabled = true;
            return webView;
        }

        protected override void ConnectHandler(WebView platformView)
        {
            base.ConnectHandler(platformView);

            platformView.Settings.JavaScriptEnabled = true;
            platformView.Settings.DomStorageEnabled = true;
            platformView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
            platformView.Settings.SetSupportMultipleWindows(true);

            platformView.Settings.UserAgentString = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36";


            platformView.SetWebViewClient(new WebViewClient());

            LoadUrlWithPostData();
        }

        private void LoadUrlWithPostData()
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

            byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);
            PlatformView?.PostUrl(customWebView.Url, postDataBytes);
        }
    }
}
