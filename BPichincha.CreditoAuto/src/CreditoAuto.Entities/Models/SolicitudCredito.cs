using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Models
{
    public class SolicitudCredito
    {
        public int SolicitudId { get; set; }
        public string IdentificacionCliente { get; set; }
        public int NumeroPuntoVenta { get; set; }
        public string Placa { get; set; }
        public int MesesPlazo { get; set; }
        public int Cuotas { get; set; }
        public decimal Entrada { get; set; }
        public string IdentificacionEjecutivo { get; set; }
        public string Observacion { get; set; }
        public int Estado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Ejecutivo Ejecutivo { get; set; }
        public virtual Patio Patio { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}
