using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    public class Conferencista : BaseViewModel
    {
        readonly ClaseBase claseBase = new ClaseBase();

        public string? idConferencista { get; set; }
        public string? NombreSuceso { get; set; }
        public string? idSuceso { get; set; }
        public string? IdSucesoServicio { get; set; }
        public string? NombreConferencista { get; set; }
        public string? Profesion { get; set; }
        public string? Cita { get; set; }
        public string? Perfil { get; set; }
        public string? Palabraclave { get; set; }
        public string? PerfilConferencista { get; set; }
        public string? SitioWeb { get; set; }
        public string? Fav { get; set; }
        public string? Imagen { get; set; }
        public string? ImagenFeria { get; set; }
        public string? IdPais { get; set; }
        public string? NombrePais { get; set; }

        public string? _imagenFav { get; set; }

        public bool VerSitioWeb { get; set; }
        public string? IdAutor { get; set; }

        public string? ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }


        public Conferencista(string IdConferencista, string nombreSuceso, string IdSuceso, string idSucesoServicio, string nombreConferencista, string profesion, string cita, string perfil,
            string palabraclave, string perfilConferencista, string sitioWeb, string fav, string imagen, string imagenFeria, string idPais, string nombrePais, string idAutor)
        {
            idConferencista = IdConferencista;
            NombreSuceso = nombreSuceso;
            idSuceso = IdSuceso;
            IdSucesoServicio = idSucesoServicio;
            NombreConferencista = nombreConferencista;
            Profesion = profesion;
            Cita = cita;
            Perfil = perfil;
            Palabraclave = palabraclave;
            PerfilConferencista = perfilConferencista;
            SitioWeb = sitioWeb;
            Fav = fav;
            Imagen = imagen;
            ImagenFeria = imagenFeria;
            IdPais = idPais;
            NombrePais = nombrePais;
            IdAutor = idAutor;

            if (Fav == "1")
            {
                ImagenFav = "ic_favortio_relleno";
            }
            else
            {
                ImagenFav = "ic_favorito_obscuro";
            }

            if (claseBase.ValidaString(SitioWeb) != "")
            {
                VerSitioWeb = true;
            }

            if (Imagen == "")
            {
                Imagen = ImagenFeria;
            }
        }
    }
}