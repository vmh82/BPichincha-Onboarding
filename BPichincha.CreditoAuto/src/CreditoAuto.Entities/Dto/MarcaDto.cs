using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Dto
{
    public  class MarcaDto
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
