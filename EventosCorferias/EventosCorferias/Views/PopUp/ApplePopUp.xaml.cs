using EventosCorferias.Models;
using Mopups.Services;

namespace EventosCorferias.Views.PopUp;

public partial class ApplePopUp
{
    public ApplePopUp()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "AlertPopup", "Button_OnClicked", "Cierra popup");
        }
    }

}