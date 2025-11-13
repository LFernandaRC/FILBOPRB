using EventosCorferias.Services;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EventosCorferias.Models
{
    public class ClaseBase
    {
        public string CapitalizeFirstLetter(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "";
                if (string.IsNullOrEmpty(value))
                    return "";
                if (value.Equals(""))
                    return "";
                if (value.Equals("0"))
                    return "";
                value = value.Trim();
                return value.First().ToString().ToUpper() + value.Substring(1).ToLower();
            }
            catch
            {
                return "";
            }
        }

        public string ValidaString(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    value = "";
                if (string.IsNullOrEmpty(value))
                    value = "";
                if (value.Equals("0"))
                    value = "";
            }
            catch
            {
                value = "";
            }
            return value;
        }

        public void FocusPicker_Mto(object obj)
        {
            try
            {
                var focusPicker = (Picker)obj;
                focusPicker.Focus();
            }
            catch { }
        }

        public void FocusPickerDate_Mtd(object obj)
        {
            try
            {
                var focusPicker = (Picker)obj;
                focusPicker.Focus();
            }
            catch { }
        }

        public string FormatoPrecio_Mto(long numero)
        {
            try
            {
                string Res = "";
                int contAux = 1;
                int aunxDos = numero.ToString().Length;
                int aunxTres = aunxDos % 3;
                foreach (char chartAux in numero.ToString().Reverse())
                {
                    if (aunxTres == 0 && contAux == aunxDos)
                    {
                        Res += chartAux;
                    }
                    else
                    {
                        if (contAux % 3 == 0)
                            Res += chartAux + ".";
                        else
                            Res += chartAux;
                    }

                    contAux += 1;
                }

                string Respuesta = "";
                foreach (char chartAux in Res.Reverse())
                    Respuesta += chartAux;

                return "$ " + Respuesta + ".00";
            }
            catch
            {
                return numero.ToString();
            }
        }

        public async void InsertarLogs_Mtd(string tipoError, string msmError, string nombreVista, string nombreMetodo, string NombreServicios)
        {
            try
            {
                Debug.WriteLine(tipoError + msmError);
                LogicaWs logicaWs = new LogicaWs();

                if (tipoError == null)
                    tipoError = "ERROR";

                if (tipoError != " ")
                    tipoError = "ERROR";

                if (msmError != null)
                {
                    if (msmError != "")
                    {
                        if (msmError.Length > 150)
                            msmError = msmError.Substring(0, 148);
                    }
                    else
                    {
                        msmError = "no aplica";
                    }
                }
                else
                {
                    msmError = "no aplica";
                }

                if (nombreVista != null)
                {
                    if (nombreVista != "")
                    {
                        if (nombreVista.Length > 49)
                            nombreVista = nombreVista.Substring(0, 48);
                    }
                    else
                    {
                        nombreVista = "no aplica";
                    }
                }
                else
                {
                    nombreVista = "no aplica";
                }


                if (nombreMetodo != null)
                {
                    if (nombreMetodo != "")
                    {
                        if (nombreMetodo.Length > 49)
                            nombreMetodo = nombreMetodo.Substring(0, 48);
                    }
                    else
                    {
                        nombreMetodo = "no aplica";
                    }
                }
                else
                {
                    nombreMetodo = "no aplica";
                }


                if (NombreServicios != null)
                {
                    if (NombreServicios != "")
                    {
                        if (NombreServicios.Length > 49)
                            NombreServicios = NombreServicios.Substring(0, 48);
                    }
                    else
                    {
                        NombreServicios = "no aplica";
                    }
                }
                else
                {
                    NombreServicios = "no aplica";
                }


                string urliEx = logicaWs.Movile_Insert_Logs_Mtd();
                Logs logs = new Logs(nombreVista, tipoError + " " + msmError, nombreMetodo, NombreServicios);
                string json = JsonConvert.SerializeObject(logs);
                await logicaWs.ConectionPost(json, urliEx);
            }
            catch
            {

            }
        }

    }

    public class Images
    {
        public string Url { get; set; }
        public string Link { get; set; }
    }

    public class ConsultaAgenda
    {
        public string Categoria { get; set; }
        public string Lugar { get; set; }
        public string FechaInicio { get; set; }
        public string IdAgenda { get; set; }
        public string Franja { get; set; }
    }

    public class UptFranja
    {
        public string Franja { get; set; }
    }
}
