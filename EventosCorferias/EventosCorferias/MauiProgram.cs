using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Mopups.Hosting;

namespace EventosCorferias
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("LaNuevaRegular.ttf", "PalabrasCuerpoNueva");
                    fonts.AddFont("Montserrat-Bold.ttf", "NotoBold");
                    fonts.AddFont("Montserrat-ExtraBold.ttf", "NotoExtraBold");
                    fonts.AddFont("Montserrat-Light.ttf", "NotoLight");
                    fonts.AddFont("Montserrat-Medium.ttf", "NotoMedium");
                    fonts.AddFont("Montserrat-Regular.ttf", "NotoRegular");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "NotoSemiBold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Handler para Entry (iOS y Android)
            EntryHandler.Mapper.AppendToMapping("NoBorders", (handler, view) =>
            {
#if IOS
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.CornerRadius = 0;
            handler.PlatformView.Layer.BorderWidth = 0;
#endif

#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.SetPadding(0, 0, 0, 0);
                handler.PlatformView.Background = null;
#endif
            });

            // Handler para Picker (iOS y Android)
            PickerHandler.Mapper.AppendToMapping("NoBorders", (handler, view) =>
            {
#if IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.Layer.CornerRadius = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif

#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.SetPadding(0, 0, 0, 0);
                handler.PlatformView.Background = null;
#endif
            });

            // Handler para DatePicker (iOS y Android)
            DatePickerHandler.Mapper.AppendToMapping("NoBorders", (handler, view) =>
            {
#if IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.Layer.CornerRadius = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif

#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.SetPadding(0, 0, 0, 0);
                handler.PlatformView.Background = null;
#endif
            });

            // Handler para Editor (iOS y Android)
            EditorHandler.Mapper.AppendToMapping("NoBorders", (handler, view) =>
            {
#if IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.Layer.CornerRadius = 0;
#endif

#if ANDROID
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.SetPadding(0, 0, 0, 0);
                handler.PlatformView.Background = null;
#endif
            });


            return builder.Build();
        }
    }
}
