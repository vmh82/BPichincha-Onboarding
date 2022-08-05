using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Models
{
    public  partial class AsignacionCliente
    {
        public int AsignacionId { get; set; }
        public string Identificacion { get; set; }
        public int NumeroPuntoVenta { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Patio Patio { get; set; }
    }
}
