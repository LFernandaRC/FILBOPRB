using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    public class Expositores : BaseViewModel
    {
        public string IdExpositor { get; set; }
        public string NombreExpositor { get; set; }
        public string PerfilExpositor { get; set; }
        public string TiendaVirtual { get; set; }
        public string LogoDetalle { get; set; }
        public string ImagenDetalle { get; set; }
        public string Imagen { get; set; }
        public string Fav { get; set; }
        public string Url { get; set; }
        public string Direccion { get; set; }
        public string Pabellon { get; set; }
        public string nivel { get; set; }
        public string stand { get; set; }
        public string IdSitio { get; set; }
        public string NombreSitio { get; set; }
        public string IdMapa { get; set; }
        public string IdRecintoSuceso { get; set; }
        public string NombreSuceso { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }

        public string IdSuceso { get; set; }

        public int Columna { get; set; }
        public int Fila { get; set; }

        public string _imagenFav { get; set; }

        public string ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public Expositores(string idExpositor, string nombreExpositor, string perfilExpositor, string tiendaVirtual,
            string logoDetalle, string imagenDetalle, string imagen, string fav, string url, string direccion, string pabellon,
            string Nivel, string Stand, string idSitio, string nombreSitio, string idMapa, string idRecintoSuceso, string nombreSuceso,
            string Email, string Telefono, string idScueso, string Latitud, string Longitud)
        {
            IdExpositor = idExpositor;
            NombreExpositor = nombreExpositor;
            PerfilExpositor = perfilExpositor;
            TiendaVirtual = tiendaVirtual;
            LogoDetalle = logoDetalle;
            ImagenDetalle = imagenDetalle;
            Imagen = imagen;
            Fav = fav;
            Url = url;
            Direccion = direccion;
            Pabellon = pabellon;
            nivel = Nivel;
            stand = Stand;
            IdSitio = idSitio;
            NombreSitio = nombreSitio;
            IdMapa = idMapa;
            IdRecintoSuceso = idRecintoSuceso;
            NombreSuceso = nombreSuceso;
            email = Email;
            telefono = Telefono;
            IdSuceso = idScueso;
            latitud = Latitud;
            longitud = Longitud;

            if (Fav == "1")
                ImagenFav = "ic_favortio_relleno";
            else
                ImagenFav = "ic_favorito_obscuro";

        }
    }
}
