using System.ComponentModel.DataAnnotations;

namespace CRUDCORE.Models
{
    public class ContactoModel
    {
        public int? Id_IE { get; set; }
        public string? N_Identificacion { get; set; }
        public string? Codigo_Sae { get; set; }
        public string? Nombres { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Fec_ExpDoc { get; set; }
        public string? Email_Personal { get; set; }
        public string? Email_Empresa { get; set; }
        public string? Descripcion { get; set; }

        //-------------------------------------------//

        public int Id_Usuario { get; set; }
        public string? correo { get; set; }
        public string? clave { get; set; }
    }
}
