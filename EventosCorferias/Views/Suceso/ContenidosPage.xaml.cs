using EventosCorferias.Models;
using EventosCorferias.ViewModel.Suceso;
using EventosCorferias.Views.Usuario;

namespace EventosCorferias.Views.Suceso;

public partial class ContenidosPage : ContentPage
{
    public MasterHomePage RootMainPage => Application.Current.MainPage as MasterHomePage;
    public NavigationPage RootNavigation => RootMainPage?.Detail as NavigationPage;

    private bool CategoriaVisible;
    private string TipoContenido;
    private string IdSusesoComunidad;
    private string Vista;

    public ContenidosPage(bool categoriaVisible, string tipoContenido, string idSusesoComunidad, string vista)
    {
        InitializeComponent();
        CategoriaVisible = categoriaVisible;
        TipoContenido = tipoContenido;
        IdSusesoComunidad = idSusesoComunidad;
        Vista = vista;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BindingContext = new ContenidosVm(CategoriaVisible, TipoContenido, IdSusesoComunidad, EntryFiltro, Vista);
    }


    private async void producttablelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var content = e.CurrentSelection[0] as Contenidos;
        await RootNavigation.PushAsync(new DetalleContenidoView(content, null, Vista), false);
    }
}