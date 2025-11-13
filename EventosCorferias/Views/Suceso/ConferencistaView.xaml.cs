using EventosCorferias.Models;
using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class ConferencistaView : ContentPage
{
    private readonly string IdSuceso;
    private readonly bool EsFavorito;

    public ConferencistaView(string idSuceso, bool esfavorito)
    {
        IdSuceso = idSuceso;
        EsFavorito = esfavorito;
        InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new ConferencistaVm(IdSuceso, EsFavorito, FiltroEntry);
    }

    private void FiltroEntry_Completed(object sender, EventArgs e)
    {
        FiltroEntry.Unfocus();
    }
}