using CreditoAuto.Entities.Models;
namespace CreditoAuto.Domain.Interfaces
{
    public interface ISolicitudCreditoRepository
    {
        Task<SolicitudCredito> Consultar(int numeroSolicitud);
        Task<SolicitudCredito> Crear(SolicitudCredito solicitud);
        Task<int> Eliminar(int numeroSolicitud);
        Task<SolicitudCredito> Actualizar(SolicitudCredito solicitud);
    }

}
