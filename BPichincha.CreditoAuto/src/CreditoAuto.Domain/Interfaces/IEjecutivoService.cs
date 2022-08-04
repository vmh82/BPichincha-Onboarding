using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;


namespace CreditoAuto.Domain.Interfaces
{
    public interface IEjecutivoService
    {
        Task<Response<EjecutivoDto>> Consultar(string identificacion);
        Task<Response<EjecutivoDto>> Crear(EjecutivoDto ejecutivoRequest);
        Task<Response<int>> Eliminar(string identificacion);
        Task<Response<EjecutivoDto>> Actualizar(EjecutivoDto ejecutivoRequest);
    }
}
