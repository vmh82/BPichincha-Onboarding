using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CreditoAuto.Entities.Models
{
    public class Vehiculo
    {

        [Key]
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NumeroChasis { get; set; }
        public Marca Marca { get; set; }
        public string Tipo { get; set; }
        public decimal Cilindraje { get; set; }
        public decimal Avaluo { get; set; }
    }
}
