using EventosCorferias.Resources.RecursosIdioma;

namespace EventosCorferias.Models
{
    public class PreRegistro
    {
        public string? Cons { get; set; }
        public string? IdSuceso { get; set; }
        public string? IdPreguntaServicio { get; set; }
        public string? IdPregunta { get; set; }
        public string? IdTipoPregunta { get; set; }
        public string? TipoPregunta { get; set; }
        public string? Pregunta { get; set; }
        public string? Estado { get; set; }
        public string? ListaRespuesta { get; set; }

        public PreRegistro(string cons, string idSuceso, string idPreguntaServicio, string idPregunta,
            string idTipoPregunta, string tipoPregunta, string pregunta, string estado, string listaRespuesta)
        {
            Cons = cons;
            IdSuceso = idSuceso;
            IdPreguntaServicio = idPreguntaServicio;
            IdPregunta = idPregunta;
            IdTipoPregunta = idTipoPregunta;
            TipoPregunta = tipoPregunta;
            Pregunta = pregunta;
            Estado = estado;
            ListaRespuesta = listaRespuesta;
        }
    }

    public class PreRegistroTaquilla
    {
        private readonly ClaseBase claseBase;

        public string? Cons { get; set; }
        public string? IdSuceso { get; set; }
        public string? NombreSuceso { get; set; }
        public string? CodigoInvitacion { get; set; }
        public string? TieneInvitacion { get; set; }
        public string? EstadoSolicitud { get; set; }
        public string? CodigoQR { get; set; }
        public DateTime Vigencia { get; set; }
        public string? VigenciaString { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Identificacion { get; set; }
        public string? NombreIdentificacion { get; set; }
        public string? NombreProfesion { get; set; }
        public string? NombreRecinto { get; set; }
        public string? DiaIn { get; set; }
        public string? MesIn { get; set; }
        public bool vigenciaVista { get; set; }

        public string? ImagenFeria { get; set; }
        public bool VerCodigoQR { get; set; }
        public bool VerImagenFeria { get; set; }
        public ImageSource? QrImage { get; set; }

        public PreRegistroTaquilla(string cons, string idSuceso, string nombreSuceso, string codigoInvitacion,
            string tieneInvitacion, string estadoSolicitud, string codigoQR,
            string vigenciaString, string nombreCompleto, string identificacion, string nombreIdentificacion,
            string nombreProfesion, string nombreRecinto, string diaIn, string mesIn)
        {
            Cons = cons;
            IdSuceso = idSuceso;
            NombreSuceso = nombreSuceso;
            CodigoInvitacion = codigoInvitacion;
            TieneInvitacion = tieneInvitacion;
            EstadoSolicitud = estadoSolicitud;
            CodigoQR = codigoQR;
            VigenciaString = vigenciaString;
            NombreCompleto = nombreCompleto;
            Identificacion = identificacion;
            NombreIdentificacion = nombreIdentificacion;
            NombreProfesion = nombreProfesion;
            NombreRecinto = nombreRecinto;
            DiaIn = diaIn;
            MesIn = mesIn;

            Vigencia = DateTime.ParseExact("12/05/1945", "dd/MM/yyyy", null);
            vigenciaVista = false;

            if (VigenciaString != null)
            {
                if (VigenciaString != "" && VigenciaString != " " && VigenciaString != "0")
                {
                    Vigencia = DateTime.ParseExact(VigenciaString, "dd/MM/yyyy", null);
                    vigenciaVista = true;
                }
            }
            claseBase = new ClaseBase();
            try
            {
                if (claseBase.ValidaString(MesIn) != "")
                {
                    string[] ListaRespuesta = claseBase.ValidaString(MesIn.Trim()).Split(new string[] { " " }, System.StringSplitOptions.None);
                    switch (ListaRespuesta[0].ToLower())
                    {
                        case "enero":
                            MesIn = AppResources.enero + " " + ListaRespuesta[1];
                            break;
                        case "febrero":
                            MesIn = AppResources.febrero + " " + ListaRespuesta[1];
                            break;
                        case "marzo":
                            MesIn = AppResources.marzo + " " + ListaRespuesta[1];
                            break;
                        case "abril":
                            MesIn = AppResources.abril + " " + ListaRespuesta[1];
                            break;
                        case "mayo":
                            MesIn = AppResources.mayo + " " + ListaRespuesta[1];
                            break;
                        case "junio":
                            MesIn = AppResources.junio + " " + ListaRespuesta[1];
                            break;
                        case "julio":
                            MesIn = AppResources.julio + " " + ListaRespuesta[1];
                            break;
                        case "agosto":
                            MesIn = AppResources.agosto + " " + ListaRespuesta[1];
                            break;
                        case "septiembre":
                            MesIn = AppResources.septiembre + " " + ListaRespuesta[1];
                            break;
                        case "octubre":
                            MesIn = AppResources.octubre + " " + ListaRespuesta[1];
                            break;
                        case "noviembre":
                            MesIn = AppResources.noviembre + " " + ListaRespuesta[1];
                            break;
                        case "diciembre":
                            MesIn = AppResources.diciembre + " " + ListaRespuesta[1];
                            break;
                        default:
                            break;
                    }
                }
            }
            catch { }
        }
    }
}