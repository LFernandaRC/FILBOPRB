using EventosCorferias.Models;
using EventosCorferias.ViewModel.Shared;
#if IOS
using UIKit;
using Foundation;
#endif


namespace EventosCorferias.Views.Usuario;

public partial class LoginView : ContentPage
{
    private string? appleEmail;
    private string? appleFullName;

    public LoginView()
    {
        try
        {
            InitializeComponent();
            BindingContext = new LoginVm();
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginView", "LoginView", "Carga Inicial vista");
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (IsPasword1.IsPassword)
            {
                IsPasword1.IsPassword = false;
                ImageButton1.Source = "ic_mostrar_obscuro.png";
            }
            else
            {
                IsPasword1.IsPassword = true;
                ImageButton1.Source = "ic_ocultar_obscuro.png";
            }
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "LoginView", "ImageButton_Clicked", "n/a");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

#if IOS
    // Observar cuando aparece el teclado
    UIKeyboard.Notifications.ObserveWillShow((sender, args) =>
    {
        var keyboardHeight = args.FrameEnd.Height;

        // Asegúrate de ejecutarlo en el hilo principal
        Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
        {
            this.Padding = new Thickness(0, 0, 0, keyboardHeight);
        });
    });

    // Observar cuando se oculta el teclado
    UIKeyboard.Notifications.ObserveWillHide((sender, args) =>
    {
        Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
        {
            this.Padding = new Thickness(0);
        });
    });
#endif
    }
}
