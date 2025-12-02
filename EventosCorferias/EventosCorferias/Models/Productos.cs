using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    public class Productos : BaseViewModel
    {
        public string? IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public string? Imagen { get; set; }
        public string? Estado { get; set; }
        public string? Precio { get; set; }
        public string? Categoria { get; set; }
        public bool VisiblePor { get; set; }
        public bool VisibleDescripcion { get; set; }
        public bool VisibleCategoria { get; set; }


        public Productos(string idProducto, string nombreProducto, string dsescripcionProducto, string imagen, 
            string estado, string precio, string categoria)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            DescripcionProducto = dsescripcionProducto;
            Imagen = imagen;
            Estado = estado;
            Precio = precio;
            Categoria = categoria;

            if (Categoria == "0")
            {
                Categoria = "";
            }

            if (Precio.ToLower() == "null" || precio == "" || precio == " " || precio == "$ 0.00")
            {
                VisiblePor = false;
            }
            else
            {
                VisiblePor = true;
            }

            if (DescripcionProducto.ToLower() == "null" || DescripcionProducto == "" || DescripcionProducto == " ")
            {
                VisibleDescripcion = false;
            }
            else
            {
                VisibleDescripcion = true;
            }

            if (Categoria.ToLower() == "null" || Categoria == "" || Categoria == " ")
            {
                VisibleCategoria = false;
            }
            else
            {
                VisibleCategoria = true;
            }
        }
    }
}