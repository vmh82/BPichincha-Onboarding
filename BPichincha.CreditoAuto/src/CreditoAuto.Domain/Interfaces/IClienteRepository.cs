using CreditoAuto.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> ConsultarCliente(string identificacion);
        Task<int> CrearCliente(Cliente cliente);
        Task<int> EliminarCliente(Cliente cliente);
        Task<int> ActualizarCliente(Cliente cliente);
    }
}
