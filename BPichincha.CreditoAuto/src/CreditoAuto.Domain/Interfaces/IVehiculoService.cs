using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IVehiculoService
    {
        Task<Response<VehiculoDto>> Consultar(string placa);
        Task<Response<VehiculoDto>> Crear(VehiculoDto vehiculoRequest);
        Task<Response<int>> Eliminar(string placa);
        Task<Response<VehiculoDto>> Actualizar(VehiculoDto vehiculoRequest);
    }
}
