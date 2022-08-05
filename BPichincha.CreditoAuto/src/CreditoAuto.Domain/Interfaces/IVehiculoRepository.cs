using CreditoAuto.Entities.Models;


namespace CreditoAuto.Domain.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<Vehiculo> Consultar(string placa);
        Task<int> Crear(Vehiculo vehiculo);
        Task<int> Eliminar(Vehiculo vehiculo);
        Task<int> Actualizar(Vehiculo vehiculo);
    }
}
