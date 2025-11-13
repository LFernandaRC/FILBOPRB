using EventosCorferias.ViewModel.Suceso;

namespace EventosCorferias.Views.Suceso;

public partial class ExpositorView : ContentPage
{
    public readonly string IdSuceso;
    public readonly bool EsFavorito;

    public ExpositorView(string idSuceso, bool esFavorito)
    {
        InitializeComponent();
        IdSuceso = idSuceso;
        EsFavorito = esFavorito;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new ExpositorVm(IdSuceso, EsFavorito, EntryFiltro);
    }

}