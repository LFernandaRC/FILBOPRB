using EventosCorferias.Models;
using EventosCorferias.ViewModel.Usuario;

namespace EventosCorferias.Views.Usuario;

public partial class RegistroView : ContentPage
{
    public RegistroView()
    {
        try
        {
            InitializeComponent();
            BindingContext = new RegistroVM();
        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "RegistroView", "RegistroView", "Inicializar vista");
        }
    }
}