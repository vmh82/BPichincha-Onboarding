
namespace CreditoAuto.Entities.Models
{
    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NumeroChasis { get; set; }
        public int MarcaId { get; set; }
        public virtual Marca Marca { get; set; }
        public string Tipo { get; set; }
        public decimal Cilindraje { get; set; }
        public decimal Avaluo { get; set; }
    }
}
