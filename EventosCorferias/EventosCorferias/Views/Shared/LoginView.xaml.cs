using EventosCorferias.Models;
using EventosCorferias.ViewModel.Shared;

namespace EventosCorferias.Views.Shared;

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
    }
}