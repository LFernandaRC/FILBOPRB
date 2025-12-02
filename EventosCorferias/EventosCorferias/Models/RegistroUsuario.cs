namespace EventosCorferias.Models
{
    public class RegistroUsuario
    {
        public string? Email { get; set; }
        public string? Contrasena { get; set; }
        public string? Identificacion { get; set; }
        public int IdIdentificacion { get; set; }
        public string? NombreCompleto { get; set; }
        public int IdRedSocial { get; set; }
        public string? TokenRed { get; set; }
        public int TerminosCondiciones { get; set; }
        public string? FechaNacimiento { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string? Celular { get; set; }
        public int IdProfesion { get; set; }
        public int IdSectorEconomico { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? IdentificacionEmpresa { get; set; }
        public int IndicativoPais { get; set; }
        public int Area { get; set; }
        public string? Telefono { get; set; }
        public int IdPaisemp { get; set; }
        public int IdCiudademp { get; set; }
        public int CodigoPostal { get; set; }
        public string? Cargo { get; set; }
        public int IdSectorEconomicoemp { get; set; }
        public string? Direccion { get; set; }
        public string? Imagen { get; set; }
    }

    public class ActualizarUsuario
    {
        public string? NombreCompleto { get; set; }
        public string? FechaNacimiento { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string? Celular { get; set; } // comillas
        public int IdProfesion { get; set; }
        public int IdSectorEconomico { get; set; }
        public string? Imagen { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? IdentificacionEmpresa { get; set; }
        public int IndicativoPais { get; set; }
        public int Area { get; set; }
        public string? Telefono { get; set; }
        public int IdPaisemp { get; set; }
        public int IdCiudademp { get; set; }
        public int CodigoPostal { get; set; }
        public string? Cargo { get; set; }
        public int IdSectorEconomicoemp { get; set; }
        public string? Direccion { get; set; }
    }

    public class ActualizarInsertaUsuario
    {


        public string? NombreCompleto { get; set; }
        public string? FechaNacimiento { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string? Celular { get; set; } // comillas
        public int IdProfesion { get; set; }
        public int IdSectorEconomico { get; set; }
        public string? Imagen { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? IdentificacionEmpresa { get; set; }
        public int IndicativoPais { get; set; }
        public int Area { get; set; }
        public string? Telefono { get; set; }
        public int IdPaisemp { get; set; }
        public int IdCiudademp { get; set; }
        public int CodigoPostal { get; set; }
        public string? Cargo { get; set; }
        public int IdSectorEconomicoemp { get; set; }
        public string? Direccion { get; set; }

        //DATOS VIEJOS
        public string? IdIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? Email { get; set; } //correo nuevo
        public string? Contrasena { get; set; }
        public string? TerminosCondiciones { get; set; }
    }

}