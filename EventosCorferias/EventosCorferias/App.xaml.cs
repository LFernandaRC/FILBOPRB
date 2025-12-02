using EventosCorferias.Views.Usuario;

namespace EventosCorferias
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new Splash();
        }

    }
}