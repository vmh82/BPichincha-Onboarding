using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;


namespace CreditoAuto.Domain.Interfaces
{
    public interface IAsignacionClienteService
    {
        Task<Response<ClientePatioDto>> Consultar(string identificacion);
        Task<Response<ClientePatioDto>> Crear(AsignacionClienteDto asignacionRequest);
        Task<Response<int>> Eliminar(string identificacion);
        Task<Response<AsignacionClienteDto>> Actualizar(AsignacionClienteDto asignacionRequest);
    }
}
