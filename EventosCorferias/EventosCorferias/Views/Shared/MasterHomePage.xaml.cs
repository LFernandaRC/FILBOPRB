using EventosCorferias.Models;
using Shiny;
using Shiny.Hosting;
using Shiny.Push;
using System.ComponentModel;

using EventosCorferias.Models;
using Shiny;
using Shiny.Hosting;
using Shiny.Push;
using EventosCorferias.Services;


#if ANDROID
using Android;
using Android.Content.PM;
#endif


namespace EventosCorferias.Views.Usuario;

[DesignTimeVisible(false)]
public partial class MasterHomePage : FlyoutPage
{
    bool _pushSolicitado;

    public MasterHomePage()
    {
        try
        {
            InitializeComponent();
            BindingContext = this;
            Flyout.Title = "";

        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MasterHomePage", "MasterHomePage", "n/a");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_pushSolicitado)
            return;

        _pushSolicitado = true;
        await RegistrarPush();
    }

    private async Task RegistrarPush()
    {
        try
        {
#if ANDROID
            if (OperatingSystem.IsAndroidVersionAtLeast(33))
            {
                var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.PostNotifications>();
                }
            }
#endif

            var push = Host.Current.Services.GetService<IPushManager>();

            var result = await push.RequestAccess();

            if (result.Status == AccessState.Available)
            {
                var token = result.RegistrationToken;
                Preferences.Set("TokenFirebase", token);

                var EmailFire = Preferences.Get("Email", "");
                var idApp = Preferences.Get("IdApp", "");

                if (EmailFire != null && Preferences.Get("TokenFirebase", "") != null)
                {
                    LogicaWs logicaWS = new LogicaWs();
                    string urli = logicaWS.Movile_Update_Token_Mtd(EmailFire, Preferences.Get("TokenFirebase", ""), idApp);
                    string jsonProcedimiento = await logicaWS.ConectionGet(urli);
                }

                ClaseBase claseBase;
                claseBase = new ClaseBase();
                claseBase.InsertarLogs_Mtd("ERROR", token, "SPALR FIREBASE", "SPALR FIREBASE", "SPALR FIREBASE");

            }
            else
            {
                ClaseBase claseBase;
                claseBase = new ClaseBase();
                claseBase.InsertarLogs_Mtd("ERROR", $"Estado: {result.Status}", "SPALR FIREBASE", "SPALR FIREBASE", "SPALR FIREBASE");
            }
        }
        catch (Exception s)
        {
            ClaseBase claseBase;
            claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", s.Message, "SPALR FIREBASE", "SPALR FIREBASE", "SPALR FIREBASE");

        }

    }
}
