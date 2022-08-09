using CreditoAuto.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IAsignacionClienteRepository
    {
        Task<AsignacionCliente> Consultar(string identificacion, int numeroPuntoVenta);
        Task<int> Crear(AsignacionCliente asignacionCliente);
        Task<int> Eliminar(AsignacionCliente asignacionCliente);
        Task<int> Actualizar(AsignacionCliente asignacionCliente);
    }
}
