using CreditoAuto.Entities.Models;

namespace CreditoAuto.Domain.Interfaces
{
    public interface IEjecutivoRepository
    {
        Task<Ejecutivo> Consultar(string identificacion);
    }

}
