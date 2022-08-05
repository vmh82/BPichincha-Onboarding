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
    public class PatioRepository : IPatioRepository
    {
        private readonly CreditoAutoDbContext _context;
        public PatioRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Actualizar(Patio Patio)
        {
            _context.Update(Patio);
            return await _context.SaveChangesAsync();
        }

        public async Task<Patio> Consultar(int numeroPuntoVenta)
        {
            Patio? Patio = await _context.Patios.AsNoTracking().Where(q => q.NumeroPuntoVenta.Equals(numeroPuntoVenta)).Include(q=>q.AsignacionClientes).Include(q=>q.SolicitudCreditos).FirstOrDefaultAsync();
            return Patio;
        }

        public async Task<int> Crear(Patio Patio)
        {
           _context.Set<Patio>().Add(Patio);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(Patio Patio)
        {
            _context.Patios.Remove(Patio);
            return await _context.SaveChangesAsync();
        }
    }
}
