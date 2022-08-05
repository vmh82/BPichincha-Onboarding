using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Dto
{
    public  class SolicitudCreditoDto
    {
        public string IdentificacionCliente { get; set; }
        public int NumeroPuntoVenta { get; set; }
        public string Placa { get; set; }
        public int MesesPlazo { get; set; }
        public int Cuotas { get; set; }
        public decimal Entrada { get; set; }
        public string IdentificacionEjecutivo { get; set; }
        public string Observacion { get; set; }
        public string? Estado { get; set; }

    }
}
