using EventosCorferias.ViewModel.Usuario;
#if IOS
using UIKit;
using Foundation;
#endif


namespace EventosCorferias.Views.Usuario;

public partial class RecuperarClavePage : ContentPage
{
	public RecuperarClavePage()
	{
		InitializeComponent();
		BindingContext = new RecuperarClaveVm();
	}

    private void EntCod1_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        try
        {
            if (entry.Text.Length > 0)
                EntCod2.Focus();
        }
        catch
        {
        }
    }

    private void EntCod2_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        try
        {
            if (entry.Text.Length > 0)
                EntCod3.Focus();
        }
        catch
        {
        }
    }

    private void EntCod3_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;

        try
        {
            if (entry.Text.Length > 0)
                EntCod4.Focus();
        }
        catch
        {
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