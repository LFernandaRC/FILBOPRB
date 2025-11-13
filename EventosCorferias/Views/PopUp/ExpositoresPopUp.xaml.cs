using EventosCorferias.ViewModel.PopUp;
using Mopups.Services;

namespace EventosCorferias.Views.PopUp;

public partial class ExpositoresPopUp 
{
	public ExpositoresPopUp(string idSitio, string NombreServicio)
	{
		InitializeComponent();
        BindingContext = new ExpositoresPopUpVm(Preferences.Get("IdSuceso", ""), EntryFiltro, idSitio, NombreServicio);
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