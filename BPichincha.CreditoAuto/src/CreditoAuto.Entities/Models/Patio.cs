using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Models
{
    public class Patio
    {
        public Patio()
        {
            AsignacionClientes = new HashSet<AsignacionCliente>();
            Ejecutivos = new HashSet<Ejecutivo>();
            
        }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int NumeroPuntoVenta { get; set; }

        public virtual ICollection<Ejecutivo> Ejecutivos { get; set; }
        public virtual ICollection<AsignacionCliente> AsignacionClientes { get; set; }
    }
}
