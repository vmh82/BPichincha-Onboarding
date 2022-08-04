
using System.ComponentModel.DataAnnotations;

namespace CreditoAuto.Entities.Dto
{
    public  class VehiculoDto
    {
        [Required(ErrorMessage = "Identificacion Requerida")]
        public string Identificacion { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public int? Edad { get; set; } 
        [Required]
        public string FechaNacimiento { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string EstadoCivil { get; set; }
        [Required]
        public string IdentificacionConyugue { get; set; }
        [Required]
        public string NombreConyugue { get; set; }
        [Required]
        public string SujetoCredito { get; set; }

    }
}
