using CreditoAuto.Entities.Models;


namespace CreditoAuto.Domain.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<Vehiculo> Consultar(string placa);
        Task<int> Crear(Vehiculo vehilculo);
        Task<int> Eliminar(Vehiculo vehilculo);
        Task<int> Actualizar(Vehiculo vehilculo);
    }
}
