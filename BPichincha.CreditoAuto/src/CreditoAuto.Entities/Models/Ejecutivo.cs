using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Models
{
    public class Ejecutivo
    {
        [Key]
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string TelefonoConvencional { get; set; }
        public string Celular { get; set; }
        public int NumeroPuntoVenta { get; set; }
        public int Edad { get; set; }
        public virtual Patio Patio { get; set; }
    }
}
