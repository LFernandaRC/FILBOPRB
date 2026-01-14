using EventosCorferias.Services;
using EventosCorferias.Interfaces;

namespace EventosCorferias.ViewModel.Shared
{
    public class GeneralVm : BaseViewModel
    {
        public readonly IPageServicio pageServicio;
        private readonly LogicaWs logicaWs;
        public string IdCiudadRecinto;

        public GeneralVm()
        {
            logicaWs = new LogicaWs();
            pageServicio = new PageServicio();

            EmailUsuario = Preferences.Get("Email", "");
            IdCiudadRecinto = Preferences.Get("IdCiudad", "");
            CiudadRecinto = Preferences.Get("IdCiudadDesc", "");
            LenguajeBase = Preferences.Get("IdiomaDefecto", "");
            NombreCompletoPerfil = Preferences.Get("NombreCompleto", "");
            ImagenSplash = logicaWs.ImgMenuSuperior_Mtd();
            ContadorNotificaciones_Mtd();

        }
    }
}
