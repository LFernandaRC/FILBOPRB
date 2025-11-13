using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventosCorferias.Models
{
    public class UsuarioLogin
    {
        public string Contrasena { get; set; }

        public UsuarioLogin(string contrasena)
        {
            Contrasena = contrasena;
        }
    }
}
