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
    public class MarcaRepository : IClienteRepository
    {
        private readonly CreditoAutoDbContext _context;
        public MarcaRepository(CreditoAutoDbContext context)
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
            Cliente? cliente = await _context.Cliente.AsNoTracking().Where(q => q.Identificacion.Equals(identificacion)).FirstOrDefaultAsync();
            return cliente;
        }

        public async Task<int> CrearCliente(Cliente cliente)
        {
           _context.Set<Cliente>().Add(cliente);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> EliminarCliente(Cliente cliente)
        {
            _context.Cliente.Remove(cliente);
            return await _context.SaveChangesAsync();
        }
    }
}
