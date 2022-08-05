using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Entities.Utils
{
    public static class SolicitudUtil
    {
        public static string VerificarEstado(int estado)
        {
            string tipoEstado = string.Empty;
            switch (estado)
            {
                case 1:
                    tipoEstado = "Registrada";
                    break;
                case 2:
                    tipoEstado = "Despachada";
                    break;
                case 3:
                    tipoEstado = "Cancelada";
                    break;
                default:
                    tipoEstado = "Registrada";
                    break;
            }
            return tipoEstado;
        }
    }
}
