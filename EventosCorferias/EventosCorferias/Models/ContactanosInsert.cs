namespace EventosCorferias.Models
{
    public class ContactanosInsert
    {
        public string correo { get; set; }
        public string TipoContacto { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }

        public ContactanosInsert(string Correo, string tipoContacto, string celular,
            string email, string asunto, string descripcion)
        {
            correo = Correo;
            TipoContacto = tipoContacto;
            Celular = celular;
            Email = email;
            Asunto = asunto;
            Descripcion = descripcion;
        }
    }
}
