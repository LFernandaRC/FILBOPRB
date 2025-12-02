using EventosCorferias.ViewModel;

namespace EventosCorferias.Models
{
    class Notificaciones : BaseViewModel
    {
        public string? IdNotificacion { get; set; }

        public string? FechaProgramada { get; set; }

        public string? Titulo { get; set; }

        public string? Texto { get; set; }

        public string? Estado { get; set; }

        public string? IdEvaluacion { get; set; }

        public string? DiaIn { get; set; }

        public string? MesIn { get; set; }

        public string? Vigencia { get; set; }

        public DateTime Fecha { get; set; }
        public string? _imagenIcono { get; set; }

        public string? ImagenIcono
        {
            get { return _imagenIcono; }
            set { _imagenIcono = value; OnPropertyChanged(nameof(ImagenIcono)); }
        }

        public Notificaciones(string idNotificacion, string fechaProgramada, string titulo, string texto, string estado, string idEvaluacion, string diaIn, string mesIn, string vigencia)
        {
            IdNotificacion = idNotificacion;
            FechaProgramada = fechaProgramada;
            Titulo = titulo;
            Texto = texto;
            Estado = estado;
            IdEvaluacion = idEvaluacion;
            DiaIn = diaIn;
            MesIn = mesIn;
            Vigencia = vigencia;

            if (Estado == "0")
            {
                ImagenIcono = "ic_notificacion_relleno_dos.png";
            }
            else
            {
                ImagenIcono = "ic_notificacion_relleno_tres.png";
            }

            if (FechaProgramada != null)
            {
                if (FechaProgramada != "" && FechaProgramada != "0" && FechaProgramada != "0")
                {
                    Fecha = DateTime.ParseExact(FechaProgramada, "dd/MM/yyyy", null);
                }
            }
        }
    }
}
