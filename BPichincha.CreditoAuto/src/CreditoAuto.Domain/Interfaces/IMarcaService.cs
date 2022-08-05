using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IMarcaService
    {
        Task<Response<MarcaDto>> Consultar(int marcaId);
        Task<Response<MarcaDto>> Crear(MarcaDto marcaRequest);
        Task<Response<int>> Eliminar(int marcaId);
        Task<Response<MarcaDto>> Actualizar(MarcaDto marcaRequest);
    }
}
