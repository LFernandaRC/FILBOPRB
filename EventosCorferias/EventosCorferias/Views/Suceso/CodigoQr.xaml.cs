namespace EventosCorferias.Views.Suceso;

public partial class CodigoQr : ContentPage
{
    public CodigoQr(ImageSource codigo)
    {
        InitializeComponent();
        CodQ.Source = codigo;
    }

}