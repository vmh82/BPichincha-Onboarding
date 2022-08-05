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
    public class EjecutivoRepository : IEjecutivoRepository
    {
        private readonly CreditoAutoDbContext _context;
        public EjecutivoRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }
        public async Task<Ejecutivo> Consultar(string identificacion)
        {
            Ejecutivo? ejecutivo = await _context.Ejecutivos.AsNoTracking().Where(q => q.Identificacion.Equals(identificacion)).FirstOrDefaultAsync();
            return ejecutivo;
        }

     
    }
}
