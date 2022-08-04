using CreditoAuto.Entities.Models;


namespace CreditoAuto.Domain.Interfaces
{
    public interface IMarcaRepository
    {
        Task<Marca> Consultar(int marcaId);
        Task<int> Crear(Marca marca);
        Task<int> Eliminar(Marca marca);
        Task<int> Actualizar(Marca marca);
    }
}
