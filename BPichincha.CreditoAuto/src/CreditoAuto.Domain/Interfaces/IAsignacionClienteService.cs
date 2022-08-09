using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;


namespace CreditoAuto.Domain.Interfaces
{
    public interface IAsignacionClienteService
    {
        Task<Response<ClientePatioDto>> Consultar(string identificacion, int numeroPuntoVenta);
        Task<Response<ClientePatioDto>> Crear(AsignacionClienteDto asignacionRequest);
        Task<Response<int>> Eliminar(string identificacion, int numeroPuntoVenta);
        Task<Response<ClientePatioDto>> Actualizar(AsignacionClienteDto asignacionRequest);
    }
}
