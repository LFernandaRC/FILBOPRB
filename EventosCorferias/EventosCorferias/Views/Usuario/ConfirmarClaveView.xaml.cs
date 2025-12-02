using EventosCorferias.ViewModel.Usuario;
#if IOS
using UIKit;
using Foundation;
#endif

namespace EventosCorferias.Views.Usuario;

public partial class ConfirmarClaveView : ContentPage
{
    public ConfirmarClaveView()
    {
        InitializeComponent();
        BindingContext = new ConfirmarClaveVm();
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (IsPasword1.IsPassword == true)
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

    private void ImageButton_Clicked1(object sender, EventArgs e)
    {
        if (IsPasword2.IsPassword == true)
        {
            IsPasword2.IsPassword = false;
            ImageButton2.Source = "ic_mostrar_obscuro.png";
        }
        else
        {
            IsPasword2.IsPassword = true;
            ImageButton2.Source = "ic_ocultar_obscuro.png";
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