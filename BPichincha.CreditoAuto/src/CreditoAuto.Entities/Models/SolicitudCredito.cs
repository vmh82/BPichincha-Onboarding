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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CreditoId { get; set; }
        [Required]
        public DateTime FechaElaboracion { get; set; }

        [ForeignKey("Cliente")]
        public string ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [ForeignKey("Patio")]
        public int PatioId { get; set; }
        public Patio Patio { get; set; }

        [ForeignKey("Vehiculo")]
        public string Placa { get; set; }
        public Vehiculo Vehiculo { get; set; }

        public int MesesPlazo { get; set; }
        public decimal Cuotas { get; set; }
        public decimal Entrada { get; set; }

        [ForeignKey("Ejecutivo")]
        public string EjecutivoId { get; set; }
        public Ejecutivo Ejecutivo { get; set; }

        public string Observacion { get; set; }
    }
}
