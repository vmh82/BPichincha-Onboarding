using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
namespace CreditoAuto.Domain.Interfaces
{
    public interface IPatioService
    {
        Task<Response<PatioDto>> Consultar(int numeroPuntoVenta);
        Task<Response<PatioDto>> Crear(PatioDto patioRequest);
        Task<Response<int>> Eliminar(int numeroPuntoVenta);
        Task<Response<PatioDto>> Actualizar(PatioDto patioRequest);
    }

}
