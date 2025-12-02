namespace EventosCorferias.Models
{
    public class Boletas
    {
        public string? IdCategoria { get; set; }
        public string? IdActividad { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreCategoria { get; set; }
        public string? DetalleCategoria { get; set; }
        public string? Valor { get; set; }
        public string? Iva { get; set; }
        public string? Item { get; set; }
        public string? IdSuceso { get; set; }
        public string? NombreSuceso { get; set; }
        public string? Limite { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? HoraInicio { get; set; }
        public string? HoraFin { get; set; }
        public string? IdBoleteria { get; set; }
        public string? DiaInicio { get; set; }
        public string? MesInici { get; set; }
        public string? DiaFin { get; set; }
        public string? MesFin { get; set; }
        public string? imagen { get; set; }

        public string? IdSucesoServicio { get; set; }
        public string? TipoPublico { get; set; }


        public string? cantidadMaxDos { get; set; }
        public string? valorSinFormato { get; set; }

        public Boletas()
        {

        }
    }
}