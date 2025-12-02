using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    public class Cuponera : BaseViewModel
    {
        public string? IdExpositor { get; set; }
        public string? IdCupon { get; set; }
        public string? Imagen { get; set; }
        public string? NombreCategoria { get; set; }
        public string? NombreSubcategoria { get; set; }
        public string? Precio { get; set; }
        public string? Fav { get; set; }
        public string? DescripcionCupon { get; set; }
        public string? ParteLegal { get; set; }
        public string? Vigencia { get; set; }
        public string? NombreCupon { get; set; }
        public string? IdCategoriaP { get; set; }
        public string? Expositor { get; set; }
        public string? Feria { get; set; }
        public string? _imagenFav { get; set; }

        public bool ImagenV;
        public bool NombreExpositorV;
        public bool NombreCategoriaV;
        public bool NombreSubcategoriaV;
        public bool PrecioV;
        public bool NombreCuponV;
        public bool DescripcionCuponV;
        public bool VigenciaV;
        public bool ParteLegalV;

        public string? ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }

        }

        public Cuponera(string idExpositor, string idCupon, string imagen, string nombreCategoria, string nombreSubcategoria,
            string precio, string fav, string descripcionCupon, string parteLegal, string vigencia, string nombreCupon,
            string idCategoriaP, string expositor, string feria)
        {
            IdExpositor = idExpositor;
            IdCupon = idCupon;
            Imagen = imagen;
            Feria = feria;
            if (Imagen != null && Imagen != "" && Imagen != " ")
            {
                ImagenV = true;
            }
            if (NombreCategoria == "0")
            {
                NombreCategoria = "";

            }
            NombreCategoria = nombreCategoria;
            if (NombreCategoria != null && NombreCategoria != "" && NombreCategoria != " ")
            {
                NombreCategoriaV = true;
            }
            NombreSubcategoria = nombreSubcategoria;
            if (NombreSubcategoria != null && NombreSubcategoria != "" && NombreSubcategoria != " ")
            {
                NombreSubcategoriaV = true;
            }
            Precio = precio;
            if (Precio != null && Precio != "" && Precio != " ")
            {
                PrecioV = true;
            }
            Fav = fav;
            DescripcionCupon = descripcionCupon;
            if (DescripcionCupon != null && DescripcionCupon != "" && DescripcionCupon != " ")
            {
                DescripcionCuponV = true;
            }
            ParteLegal = parteLegal;
            if (ParteLegal != null && ParteLegal != "" && ParteLegal != " ")
            {
                ParteLegalV = true;
            }
            Vigencia = vigencia;
            if (Vigencia != null && Vigencia != "" && Vigencia != " ")
            {
                VigenciaV = true;
            }
            NombreCupon = nombreCupon;
            if (NombreCupon != null && NombreCupon != "" && NombreCupon != " ")
            {
                NombreCuponV = true;
            }
            IdCategoriaP = idCategoriaP;

            Expositor = expositor;
            if (Expositor != null && Expositor != "" && Expositor != " ")
            {
                NombreExpositorV = true;
            }

            Imagen = imagen;

            if (Fav == "1")
            {
                ImagenFav = "ic_favortio_relleno.png";
            }
            else
            {
                ImagenFav = "ic_favorito_obscuro.png";
            }
        }
    }
}
