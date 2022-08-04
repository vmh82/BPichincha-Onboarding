using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
namespace CreditoAuto.Domain.Interfaces
{
    public interface IPatioService
    {
        Task<Response<PatioDto>> Consultar(int codigoPatio);
        Task<Response<PatioDto>> Crear(PatioDto patioRequest);
        Task<Response<int>> Eliminar(int codigoPatio);
        Task<Response<PatioDto>> Actualizar(PatioDto patioRequest);
    }

}
