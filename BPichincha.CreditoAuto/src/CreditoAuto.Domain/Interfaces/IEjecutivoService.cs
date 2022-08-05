using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
namespace CreditoAuto.Domain.Interfaces
{
    public interface IEjecutivoService
    {
        Task<Response<EjecutivoDto>> Consultar(string identificacion);
    }

}
