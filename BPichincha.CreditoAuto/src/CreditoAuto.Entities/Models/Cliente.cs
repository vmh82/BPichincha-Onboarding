using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditoAuto.Entities.Models
{
    public class Cliente
    {
        public Cliente()
        {
            AsignacionClientes = new HashSet<AsignacionCliente>();
        }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string EstadoCivil { get; set; }
        public string IdentificacionConyugue { get; set; }
        public string NombreConyugue { get; set; }
        public string SujetoCredito { get; set; }

        public virtual ICollection<AsignacionCliente> AsignacionClientes { get; set; }

    }
}
