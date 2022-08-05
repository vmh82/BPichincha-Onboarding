using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Models;
using CreditoAuto.Repository.Context;
using Microsoft.EntityFrameworkCore;


namespace CreditoAuto.Repository
{
    public class SolicitudCreditoRepository : ISolicitudCreditoRepository
    {
        private readonly CreditoAutoDbContext _context;
        public SolicitudCreditoRepository(CreditoAutoDbContext context)
        {
            _context = context;
        }

        public async Task<int> Actualizar(SolicitudCredito solicitud)
        {
            _context.Update(solicitud);
            return await _context.SaveChangesAsync();
        }

        public async Task<SolicitudCredito> Consultar(string identificacion)
        {
            SolicitudCredito? solicitud = await _context.SolicitudCreditos.AsNoTracking().Where(q => q.IdentificacionCliente.Equals(identificacion)
            && q.FechaSolicitud.Date == DateTime.Now.Date).FirstOrDefaultAsync();
            return solicitud;
        }

        public async Task<int> Crear(SolicitudCredito solicitud)
        {
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.Estado = 1;
           _context.Set<SolicitudCredito>().Add(solicitud);
           return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(SolicitudCredito solicitud)
        {
            _context.SolicitudCreditos.Remove(solicitud);
            return await _context.SaveChangesAsync();
        }

        public async Task<SolicitudCredito> ValidarSolicitudPorDia(SolicitudCredito solicitud)
        {
            SolicitudCredito? solicitudCredito = await _context.SolicitudCreditos.Where(q => q.IdentificacionCliente.Equals(solicitud.IdentificacionCliente)
            && q.NumeroPuntoVenta.Equals(solicitud.NumeroPuntoVenta)
            && q.FechaSolicitud.Date == DateTime.Now.Date && q.Estado.Equals(1)).FirstOrDefaultAsync();
            return solicitudCredito;
        }
    }
}
