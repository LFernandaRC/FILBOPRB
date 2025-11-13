using EventosCorferias.Interfaces;
using EventosCorferias.ViewModel.PopUp;
using Mopups.Services;

namespace EventosCorferias.Views.PopUp;

public partial class AgendaSitiosPopUp
{
    private readonly IPageServicio pageServicio;

    public AgendaSitiosPopUp(string IdSitio, string IdAgenda)
    {
        InitializeComponent();
        BindingContext = new AgendaSitiosPopUpVm(Preferences.Get("IdSuceso", ""), IdSitio, filtroTres, IdAgenda);
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch { }
    }
}