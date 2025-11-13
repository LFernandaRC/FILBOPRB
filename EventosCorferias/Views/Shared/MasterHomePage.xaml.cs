using EventosCorferias.Models;
using System.ComponentModel;

namespace EventosCorferias.Views.Usuario;

[DesignTimeVisible(false)]
public partial class MasterHomePage : FlyoutPage
{
	public MasterHomePage()
	{
		try
		{
            InitializeComponent();
            BindingContext = this;
            Flyout.Title = "";

        }
        catch (Exception ex)
        {
            ClaseBase claseBase = new ClaseBase();
            claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "MasterHomePage", "MasterHomePage", "n/a");
        }
    }
}
