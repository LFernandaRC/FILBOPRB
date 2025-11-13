using EventosCorferias.Models;

namespace EventosCorferias.Controls
{
    public class CustomWebView : WebView
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(CustomWebView), default(string));

        public static readonly BindableProperty PagosProperty =
            BindableProperty.Create(nameof(Pagos), typeof(Pagos), typeof(CustomWebView), default(string));

        public string Url
        {
            get => (string)GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        public Pagos Pagos
        {
            get => (Pagos)GetValue(PagosProperty);
            set => SetValue(PagosProperty, value);
        }
    }
}