using CreditoAuto.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IEjecutivoRepository
    {
        Task<Ejecutivo> Consultar(string identificacion);
        Task<int> Crear(Ejecutivo ejecutivo);
        Task<int> Eliminar(string identificacion);
        Task<int> Actualizar(Ejecutivo ejecutivo);
    }
}
