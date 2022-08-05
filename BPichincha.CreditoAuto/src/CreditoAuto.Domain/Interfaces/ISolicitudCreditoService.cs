using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
namespace CreditoAuto.Domain.Interfaces
{
    public interface ISolicitudCreditoService
    {
        Task<Response<SolicitudCreditoDto>> Consultar(string identificacion);
        Task<Response<SolicitudCreditoDto>> Crear(SolicitudCreditoDto solicitudRequest);
        Task<Response<bool>> ValidarSolicitud(SolicitudCreditoDto solicitudRequest);
    }

}
