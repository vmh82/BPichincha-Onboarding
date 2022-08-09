using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Models;
using CreditoAuto.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAuto.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CreditoAutoDbContext _context;
        public ClienteRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }

        public async Task<int> ActualizarCliente(Cliente cliente)
        {
            _context.Update(cliente);
            return await _context.SaveChangesAsync();
        }

        public async Task<Cliente> ConsultarCliente(string identificacion)
        {
            Cliente? cliente = await _context.Clientes.AsNoTracking().Where(q => q.Identificacion.Equals(identificacion)).Include(q=>q.AsignacionClientes).Include(q=>q.SolicitudCreditos).FirstOrDefaultAsync();
            return cliente;
        }

        public async Task<int> CrearCliente(Cliente cliente)
        {
           _context.Set<Cliente>().Add(cliente);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> EliminarCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            return await _context.SaveChangesAsync();
        }
    }
}
