using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
namespace CreditoAuto.Domain.Interfaces
{
    public interface ISolicitudCreditoService
    {
        Task<Response<SolicitudCreditoDto>> Consultar(int numeroSolicitud);
        Task<Response<SolicitudCreditoDto>> Crear(SolicitudCreditoDto solicitudRequest);
        Task<Response<int>> Eliminar(int numeroSolicitud);
        Task<Response<SolicitudCreditoDto>> Actualizar(SolicitudCreditoDto solicitudRequest);
    }

}
