namespace EventosCorferias.Models
{
    public class CompraBoleteria
    {
        public string? IdSuceso { get; set; }
        public string? NombreSuceso { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? Fav { get; set; }
        public string? DiaIn { get; set; }
        public string? MesIn { get; set; }
        public string? DiaFin { get; set; }
        public string? MesFin { get; set; }
        public string? IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public string? imagen { get; set; }
        public string? verdetalle { get; set; }
        public DateTime FechaFiltroIncio { get; set; }
        public DateTime FechaFiltroFin { get; set; }
        public string? IdCategoria { get; set; }
        public string DesMesInicio { get; set; }
        public string DesMesFin { get; set; }

        public CompraBoleteria(string FechaFInicio, string FechaFFin, string MesInicio, string MesFin)
        {
            // Convertir la fecha de inicio si es válida
            if (!string.IsNullOrWhiteSpace(FechaFInicio) && FechaFInicio != "0")
            {
                FechaFiltroIncio = DateTime.ParseExact(FechaFInicio, "dd/MM/yyyy", null);
            }

            // Convertir la fecha de fin si es válida
            if (!string.IsNullOrWhiteSpace(FechaFFin) && FechaFFin != "0")
            {
                FechaFiltroFin = DateTime.ParseExact(FechaFFin, "dd/MM/yyyy", null);
            }

            // Convertir número de mes en nombre del mes
            DesMesInicio = ObtenerNombreMes(MesInicio);
            DesMesFin = ObtenerNombreMes(MesFin);
        }

        private string ObtenerNombreMes(string numeroMes)
        {
            return numeroMes;
            /*return numeroMes switch
            {
                "1" => "Enero",
                "2" => "Febrero",
                "3" => "Marzo",
                "4" => "Abril",
                "5" => "Mayo",
                "6" => "Junio",
                "7" => "Julio",
                "8" => "Agosto",
                "9" => "Septiembre",
                "10" => "Octubre",
                "11" => "Noviembre",
                "12" => "Diciembre",
                _ => "Mes inválido"
            };*/
        }
    }
}