
using System.ComponentModel.DataAnnotations;

namespace CreditoAuto.Entities.Dto
{
    public  class VehiculoDto
    {
        [Required]
        public string Placa { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string NumeroChasis { get; set; }
        [Required]
        public int MarcaId { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public decimal Cilindraje { get; set; }
        [Required]
        public decimal Avaluo { get; set; }

    }
}
