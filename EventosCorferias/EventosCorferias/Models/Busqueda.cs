using System;
using System.Collections.Generic;
using EventosCorferias.Resources.RecursosIdioma;

namespace EventosCorferias.Models
{
    public class Busqueda
    {
        private readonly ClaseBase claseBase = new ClaseBase();

        public string Modulo { get; set; }
        public string NombreModulo { get; set; }
        public string Id { get; set; }
        public string Suceso { get; set; }
        public string IdSuceso { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string DiaIn { get; set; }
        public string MesIn { get; set; }
        public string VigIn { get; set; }
        public string DiaFin { get; set; }
        public string MesFin { get; set; }
        public string VigFin { get; set; }
        public string Imagen { get; set; }
        public bool VerModulo { get; set; }
        public bool VerTiulo { get; set; }
        public bool VerSuceso { get; set; }
        public bool VerDescri { get; set; }
        public bool Verfecha { get; set; }

        public Busqueda(string modulo, string nombreModulo, string id, string suceso, string idSuceso, string titulo, string descripcion,
                        string diaIn, string mesIn, string vigIn, string diaFin, string mesFin, string vigFin, string imagen)
        {
            try
            {
                Modulo = modulo;
                NombreModulo = nombreModulo;
                Id = id;
                Suceso = suceso;
                IdSuceso = idSuceso;
                Titulo = titulo;
                Descripcion = descripcion;
                DiaIn = diaIn;
                MesIn = ObtenerNombreMes(mesIn);
                VigIn = vigIn;
                DiaFin = diaFin;
                MesFin = ObtenerNombreMes(mesFin);
                VigFin = vigFin;
                Imagen = imagen;

                VerModulo = !string.IsNullOrWhiteSpace(claseBase.ValidaString(NombreModulo));
                VerTiulo = !string.IsNullOrWhiteSpace(claseBase.ValidaString(Titulo));
                VerSuceso = !string.IsNullOrWhiteSpace(claseBase.ValidaString(Suceso));
                VerDescri = !string.IsNullOrWhiteSpace(claseBase.ValidaString(Descripcion));
                Verfecha = !string.IsNullOrWhiteSpace(claseBase.ValidaString(diaFin));
            }
            catch (Exception ex)
            {
                claseBase.InsertarLogs_Mtd("ERROR", ex.Message, "Busqueda", "Constructor", "n/a");
            }
        }

        private string ObtenerNombreMes(string mes)
        {
            Dictionary<string, string> meses = new()
            {
                { "1", AppResources.enero }, { "2", AppResources.febrero }, { "3", AppResources.marzo },
                { "4", AppResources.abril }, { "5", AppResources.mayo }, { "6", AppResources.junio },
                { "7", AppResources.julio }, { "8", AppResources.agosto }, { "9", AppResources.septiembre },
                { "10", AppResources.octubre }, { "11", AppResources.noviembre }, { "12", AppResources.diciembre }
            };

            return meses.TryGetValue(mes, out string? nombreMes) ? nombreMes : mes;
        }
    }
}
