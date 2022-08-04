using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
namespace CreditoAuto.Domain.Interfaces
{
    public interface IClienteService
    {
        Task<Response<ClienteDto>> ConsultarCliente(string identificacion);
        Task<Response<ClienteDto>> CrearCliente(ClienteDto clienteRequest);
        Task<Response<int>> EliminarCliente(string identificacion);
        Task<Response<ClienteDto>> ActualizarCliente(ClienteDto clienteRequest);
    }

}
