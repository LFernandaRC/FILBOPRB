using Mopups.Services;

namespace EventosCorferias.Views.PopUp;

public partial class AyudaPopUp
{
    public AyudaPopUp()
    {
        InitializeComponent();
        CerrarModal();
    }

    public async void CerrarModal()
    {
        try
        {
            await MopupService.Instance.PopAsync();
        }
        catch (Exception ex)
        {

        }
    }
}