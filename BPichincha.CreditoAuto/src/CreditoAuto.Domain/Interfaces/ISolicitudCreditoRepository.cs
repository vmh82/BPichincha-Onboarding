using CreditoAuto.Entities.Models;
namespace CreditoAuto.Domain.Interfaces
{
    public interface ISolicitudCreditoRepository
    {
        Task<SolicitudCredito> Consultar(string identificacion);
        Task<int> Crear(SolicitudCredito solicitud);
        Task<int> Eliminar(SolicitudCredito solicitud);
        Task<int> Actualizar(SolicitudCredito solicitud);

        Task<SolicitudCredito> ValidarSolicitudPorDia(SolicitudCredito solicitud);
    }

}
