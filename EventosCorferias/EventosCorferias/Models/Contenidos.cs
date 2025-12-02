using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    public class Contenidos : BaseViewModel
    {
        public string IdContenido { get; set; }
        public string Titulo { get; set; }
        public string NombreCategoria { get; set; }
        public string url { get; set; }
        public string ImagenPortada { get; set; }
        public string ImagenesCarrusel { get; set; }
        public string Contexto { get; set; }
        public string Contenido { get; set; }
        public string PalabrasClave { get; set; }

        public string Fav { get; set; }
        public string _imagenFav { get; set; }

        public string idSuseso { get; set; }

        public string ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public Contenidos(string idContenido, string titulo, string nombreCategoria, string Url, string imagenPortada, string imagenesCarrusel,
          string contexto, string contenido, string palabrasClave, string fav, string idSuseso)
        {
            IdContenido = idContenido;
            Titulo = titulo;
            NombreCategoria = nombreCategoria;
            if (NombreCategoria == "0")
            {
                NombreCategoria = "";
            }
            url = Url;
            ImagenPortada = imagenPortada;
            ImagenesCarrusel = imagenesCarrusel;
            Contexto = contexto;
            Contenido = contenido;
            PalabrasClave = palabrasClave;
            Fav = fav;

            if (Fav == "1")
            {
                ImagenFav = "ic_favortio_relleno";
            }
            else
            {
                ImagenFav = "ic_favorito_obscuro";
            }

            this.idSuseso = idSuseso;
        }
    }
}
