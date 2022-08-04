using CreditoAuto.Entities.Models;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IPatioRepository
    {
        Task<Patio> Consultar(int codigoPatio);
        Task<int> Crear(Patio patio);
        Task<int> Eliminar(Patio patio);
        Task<int> Actualizar(Patio patio);
    }
}
