using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Utils
{
    public class CreditoAutoException : Exception
    {
        public CreditoAutoException()
        {
        }

        public CreditoAutoException(string message) : base(message)
        {
        }
    }
}
