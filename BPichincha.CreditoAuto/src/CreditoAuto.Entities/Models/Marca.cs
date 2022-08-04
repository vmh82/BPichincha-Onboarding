using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Models
{
    public class Marca
    {
        public Marca()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MarcaId { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
