namespace EventosCorferias.Models
{
    public class MisBoletas
    {
        public string? ID { get; set; }
        public string? IdSuceso { get; set; }
        public string? NombreSuceso { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreCategoria { get; set; }
        public string? Valor { get; set; }
        public string? Cantidad { get; set; }
        public string? CodigoDescuento { get; set; }
        public string? AplicaImpuesto { get; set; }
        public string? DetalleImpuesto { get; set; }
        public string? Precio { get; set; }
        public string? TitularFacturacion { get; set; }
        public string? NombreIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? IdIdentificacion { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? IdTransaccion { get; set; }
        public string? IdBoleteria { get; set; }
        public string? NoBoleta { get; set; }
        public string? EstadoBoleta { get; set; }
        public string? DescripcionEstado { get; set; }
        public string? FechaTransaccion { get; set; }
        public string? Qr { get; set; }
        public string? FechaIngreso { get; set; }
        public string? IdCiudad { get; set; }
        public DateTime FechaIngresoFiltro { get; set; }

        public bool VerCodigoQR { get; set; }
        public bool VerImagenFeria { get; set; }
        public string? ImagenFeria { get; set; }
        public ImageSource? QrImage { get; set; }

        public MisBoletas(string fechaIngreso)
        {
            if (fechaIngreso != null)
            {
                if (fechaIngreso != "" && fechaIngreso != " " && fechaIngreso != "0")
                {
                    FechaIngresoFiltro = DateTime.ParseExact(fechaIngreso, "dd/MM/yyyy", null);
                }
            }
        }
    }
}