using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Firebase;
using Shiny;

namespace EventosCorferias
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter( new[] { ShinyPushIntents.NotificationClickAction }, Categories = new[] { "android.intent.category.DEFAULT" } )]
    public class MainActivity : MauiAppCompatActivity
    {
        public static event EventHandler<(bool Success, GoogleSignInAccount account)> ResultGoogleAuth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // 🔥 INICIALIZAR FIREBASE
            var firebaseApp = FirebaseApp.InitializeApp(this);

            if (firebaseApp == null)
            {
                var options = new FirebaseOptions.Builder()
                  .SetApplicationId("1:362436346103:android:f7871cba0b989b38cdd3f2")
                  .SetApiKey("AIzaSyD4B3y5Z6jhwlrqEFlcI33yyWYmMuMBIxQ")
                  .SetProjectId("app-marca-blanca")
                  .SetGcmSenderId("362436346103") 
                  .Build();

                FirebaseApp.InitializeApp(this, options);
            }

            // 🔔 CREAR CANAL DE NOTIFICACIÓN (OBLIGATORIO ANDROID 8+)
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(
                    "default_channel", // 👈 DEBE COINCIDIR CON EL MANIFEST
                    "Notificaciones FILBO",
                    NotificationImportance.High
                )
                {
                    Description = "Notificaciones generales de FILBO"
                };

                var notificationManager =
                    (NotificationManager)GetSystemService(NotificationService);

                notificationManager.CreateNotificationChannel(channel);
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 9001)
            {
                try
                {
                    var currentAccount = await GoogleSignIn.GetSignedInAccountFromIntentAsync(data);

                    ResultGoogleAuth.Invoke(this, (currentAccount.Email != null, currentAccount));
                }
                catch (Exception ex)
                {
                    ResultGoogleAuth.Invoke(this, (false, null));
                }

            }
        }
    }
}
