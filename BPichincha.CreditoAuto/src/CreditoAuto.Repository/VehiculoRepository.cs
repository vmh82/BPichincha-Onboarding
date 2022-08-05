using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Models;
using CreditoAuto.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace CreditoAuto.Repository
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly CreditoAutoDbContext _context;
        public VehiculoRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Actualizar(Vehiculo vehiculo)
        {
            _context.Update(vehiculo);
            return await _context.SaveChangesAsync();
        }

        public async Task<Vehiculo> Consultar(string placa)
        {
            Vehiculo? vehiculo = await _context.Vehiculos.AsNoTracking().Where(q => q.Placa.Equals(placa)).Include(q => q.Marca).Include(q=>q.SolicitudCreditos).FirstOrDefaultAsync();
            return vehiculo;
        }

        public async Task<int> Crear(Vehiculo vehiculo)
        {
           _context.Set<Vehiculo>().Add(vehiculo);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(Vehiculo vehiculo)
        {
            _context.Vehiculos.Remove(vehiculo);
            return await _context.SaveChangesAsync();
        }
    }
}
