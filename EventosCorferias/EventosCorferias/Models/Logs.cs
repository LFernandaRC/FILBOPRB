using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosCorferias.Models
{
    public class Logs
    {
        public string VistaApp { get; set; }
        public string MensajeError { get; set; }
        public string Metodo { get; set; }
        public string Servicio { get; set; }

        public Logs(string vistaApp, string mensajeError, string metodo, string servicio)
        {
            VistaApp = vistaApp;
            MensajeError = mensajeError;
            Metodo = metodo;
            Servicio = servicio;
        }
    }
}
