using EventosCorferias.ViewModel.Boletas;

namespace EventosCorferias.Views.Boletas;

public partial class SeleccionBoletasView : ContentPage
{
    public SeleccionBoletasView(Models.Boletas boletas)
    {
        InitializeComponent();
        BindingContext = new SeleccionBoletasVm(boletas);
        Entry1.Focus();
    }

    private void StackLayout_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == IsVisibleProperty.PropertyName)
        {
            if (FEmpr.IsVisible)
            {
                FEmpr.FadeTo(1f, 50);
            }
        }
    }
}