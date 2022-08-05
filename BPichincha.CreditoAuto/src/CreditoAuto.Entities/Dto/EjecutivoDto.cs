using System.ComponentModel.DataAnnotations;

namespace CreditoAuto.Entities.Dto
{
    public  class EjecutivoDto
    {
        [Required(ErrorMessage = "Identificacion Requerida")]
        public string Identificacion { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        [Required]
        public string TelefonoConvencional { get; set; }
        [Required]
        public string Celular { get; set; }
        [Required]
        public int NumeroPuntoVenta { get; set; }
        [Required]
        public int Edad { get; set; }

    }
}
