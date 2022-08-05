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
    public class AsignacionClienteRepository : IAsignacionClienteRepository
    {
        private readonly CreditoAutoDbContext _context;
        public AsignacionClienteRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Actualizar(AsignacionCliente cliente)
        {
            _context.Update(cliente);
            return await _context.SaveChangesAsync();
        }

        public async Task<AsignacionCliente> Consultar(string identificacion)
        {
            AsignacionCliente? cliente = await _context.AsignacionClientes.AsNoTracking().Where(q => q.Identificacion.Equals(identificacion)).Include(q => q.Cliente).Include(q => q.Patio).FirstOrDefaultAsync();
            return cliente;
        }

        public async Task<int> Crear(AsignacionCliente asignacionCliente)
        {
           _context.Set<AsignacionCliente>().Add(asignacionCliente);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(AsignacionCliente asignacionCliente)
        {
            _context.AsignacionClientes.Remove(asignacionCliente);
            return await _context.SaveChangesAsync();
        }
    }
}
