using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Models
{
    public class Ejecutivo
    {
        [Key]
        public string Identificacion { get; set; }
    }
}
