using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Models;
using CreditoAuto.Repository.Context;
using Microsoft.EntityFrameworkCore;
namespace CreditoAuto.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly CreditoAutoDbContext _context;
        public MarcaRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Actualizar(Marca Marca)
        {
            _context.Update(Marca);
            return await _context.SaveChangesAsync();
        }

        public async Task<Marca> Consultar(int marcaId)
        {
            Marca? marca = await _context.Marcas.AsNoTracking().Where(q => q.MarcaId.Equals(marcaId)).Include(q=>q.Vehiculos).FirstOrDefaultAsync();
            return marca;
        }

        public async Task<int> Crear(Marca Marca)
        {
           _context.Set<Marca>().Add(Marca);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(Marca Marca)
        {
            _context.Marcas.Remove(Marca);
            return await _context.SaveChangesAsync();
        }
    }
}
