using Mopups.Services;
using EventosCorferias.Models;
using EventosCorferias.ViewModel.PopUp;

namespace EventosCorferias.Views.PopUp;

public partial class ServiciosPopUp
{
    public ServiciosPopUp(string Id, string origen, string IdVista)
    {
        InitializeComponent();
        BindingContext = new ServiciosPopUpVm(Id, origen);
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Cerrar();
    }

    private async void Cerrar()
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "ServiciosPopUp", "Cerrar", "Cierra popup");
        }
    }

}