
namespace CreditoAuto.Entities.Models
{
    public class Vehiculo
    {
        public Vehiculo()
        {
            SolicitudCreditos = new HashSet<SolicitudCredito>();
        }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string NumeroChasis { get; set; }
        public int MarcaId { get; set; }
        public virtual Marca Marca { get; set; }
        public string Tipo { get; set; }
        public decimal Cilindraje { get; set; }
        public decimal Avaluo { get; set; }

        public virtual ICollection<SolicitudCredito> SolicitudCreditos { get; set; }
    }
}
