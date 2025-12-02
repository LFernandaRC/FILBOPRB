using EventosCorferias.Resources.RecursosIdioma;
using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    public class Agenda : BaseViewModel
    {
        private readonly ClaseBase claseBase;

        public string? IdAgenda { get; set; }

        public string? IdSuceso { get; set; }

        public string? IdAgendaServicio { get; set; }

        public string? Titulo { get; set; }

        public string? Categoria { get; set; }

        public string? Lugar { get; set; }

        public string? DiaIn { get; set; }

        public string? MesIn { get; set; }

        public string? DiaFin { get; set; }

        public string? MesFin { get; set; }

        public string? HoraInicio { get; set; }

        public string? HoraFin { get; set; }

        public string? Aforo { get; set; }

        public string? Estado { get; set; }

        public string? Imagen { get; set; }

        public string? IconoFeria { get; set; }

        public string? ContextoClasificacion { get; set; }

        public string? Descripcion { get; set; }

        public string? PalabrasClave { get; set; }

        public string? Motivocancelacion { get; set; }

        public string? FechaCancelacion { get; set; }

        public DateTime FechaActiva { get; set; }

        public DateTime FechaInActiva { get; set; }

        public DateTime FechaInicio { get; set; }

        public List<string> Conferencistas { get; set; }

        public List<string> IDCONFERENCISTA { get; set; }

        public string? Fav { get; set; }

        public string? _imagenFav { get; set; }

        public string? Organizador { get; set; }

        public string NameList { get { return string.Join(", ", this.Conferencistas); } }

        public string? ImagenFav
        {
            get { return _imagenFav; }
            set { _imagenFav = value; OnPropertyChanged(nameof(ImagenFav)); }
        }

        public string Franja { get; set; }

        public Agenda(string idAgenda, string idSuceso, string idAgendaServicio, string titulo, 
            string categoria, string lugar, string diaIn, string mesIn, string diaFin, string mesFin,
            string horaInicio, string horaFin, string aforo, string estado, string imagen,
            string iconoFeria, string contextoClasificacion, string descripcion, string palabrasClave,
            string motivocancelacion, string fechaCancelacion, DateTime fechaActiva, DateTime fechaInActiva,
            DateTime fechaInicio, List<string> conferencistas, List<string> iDCONFERENCISTA, string fav, string franja, string organizador)
        {
            IdAgenda = idAgenda;
            IdSuceso = idSuceso;
            IdAgendaServicio = idAgendaServicio;
            Titulo = titulo;
            Categoria = categoria;
            Lugar = lugar;
            DiaIn = diaIn;
            MesIn = mesIn;
            DiaFin = diaFin;
            MesFin = mesFin;
            HoraInicio = horaInicio;
            HoraFin = horaFin;
            Aforo = aforo;
            Estado = estado;
            Imagen = imagen;
            IconoFeria = iconoFeria;
            ContextoClasificacion = contextoClasificacion;
            Descripcion = descripcion;
            PalabrasClave = palabrasClave;
            Motivocancelacion = motivocancelacion;
            FechaCancelacion = fechaCancelacion;
            FechaActiva = fechaActiva;
            FechaInActiva = fechaInActiva;
            FechaInicio = fechaInicio;
            Conferencistas = conferencistas;
            IDCONFERENCISTA = iDCONFERENCISTA;
            Franja = franja;
            Organizador = organizador;

            Fav = fav;
            if (Fav == "1")
            {
                ImagenFav = "ic_favortio_relleno";
            }
            else
            {
                ImagenFav = "ic_favorito_obscuro";
            }

            claseBase = new ClaseBase();

            try
            {
                string[] ListaRespuesta = claseBase.ValidaString(MesIn.Trim()).Split(new string[] { " " }, System.StringSplitOptions.None);
                switch (ListaRespuesta[0])
                {
                    case "1":
                        MesIn = AppResources.enero + " " + ListaRespuesta[1];
                        break;
                    case "2":
                        MesIn = AppResources.febrero + " " + ListaRespuesta[1];
                        break;
                    case "3":
                        MesIn = AppResources.marzo + " " + ListaRespuesta[1];
                        break;
                    case "4":
                        MesIn = AppResources.abril + " " + ListaRespuesta[1];
                        break;
                    case "5":
                        MesIn = AppResources.mayo + " " + ListaRespuesta[1];
                        break;
                    case "6":
                        MesIn = AppResources.junio + " " + ListaRespuesta[1];
                        break;
                    case "7":
                        MesIn = AppResources.julio + " " + ListaRespuesta[1];
                        break;
                    case "8":
                        MesIn = AppResources.agosto + " " + ListaRespuesta[1];
                        break;
                    case "9":
                        MesIn = AppResources.septiembre + " " + ListaRespuesta[1];
                        break;
                    case "10":
                        MesIn = AppResources.octubre + " " + ListaRespuesta[1];
                        break;
                    case "11":
                        MesIn = AppResources.noviembre + " " + ListaRespuesta[1];
                        break;
                    case "12":
                        MesIn = AppResources.diciembre + " " + ListaRespuesta[1];
                        break;
                    default:
                        MesIn = ListaRespuesta[0] + " " + ListaRespuesta[1];
                        break;
                }

                string[] ListaRespuesta2 = claseBase.ValidaString(MesFin.Trim()).Split(new string[] { " " }, System.StringSplitOptions.None);
                switch (ListaRespuesta2[0])
                {
                    case "1":
                        MesFin = AppResources.enero + " " + ListaRespuesta2[1];
                        break;
                    case "2":
                        MesFin = AppResources.febrero + " " + ListaRespuesta2[1];
                        break;
                    case "3":
                        MesFin = AppResources.marzo + " " + ListaRespuesta2[1];
                        break;
                    case "4":
                        MesFin = AppResources.abril + " " + ListaRespuesta2[1];
                        break;
                    case "5":
                        MesFin = AppResources.mayo + " " + ListaRespuesta2[1];
                        break;
                    case "6":
                        MesFin = AppResources.junio + " " + ListaRespuesta2[1];
                        break;
                    case "7":
                        MesFin = AppResources.julio + " " + ListaRespuesta2[1];
                        break;
                    case "8":
                        MesFin = AppResources.agosto + " " + ListaRespuesta2[1];
                        break;
                    case "9":
                        MesFin = AppResources.septiembre + " " + ListaRespuesta2[1];
                        break;
                    case "10":
                        MesFin = AppResources.octubre + " " + ListaRespuesta2[1];
                        break;
                    case "11":
                        MesFin = AppResources.noviembre + " " + ListaRespuesta2[1];
                        break;
                    case "12":
                        MesFin = AppResources.diciembre + " " + ListaRespuesta2[1];
                        break;
                    default:
                        MesFin = ListaRespuesta2[0] + " " + ListaRespuesta2[1];
                        break;
                }
            }
            catch
            {

            }
            Organizador = organizador;
        }
    }
}
