using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class SolicitudCreditoService : IClienteService
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public SolicitudCreditoService(IMapper mapper, ILogger<ClienteService> logger,IClienteRepository clienteRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _clienteRepo = clienteRepo;
        }
        public async Task<Response<ClienteDto>> ActualizarCliente(ClienteDto clienteRequest)
        {
            try
            {
                Response<ClienteDto>? clienteDto = await ConsultarCliente(clienteRequest.Identificacion);
                if (!string.IsNullOrEmpty(clienteDto.Data.Identificacion))
                {
                    Cliente cliente = await _mapper.From(clienteRequest).AdaptToTypeAsync<Cliente>();
                    int esFinTransaccion = await _clienteRepo.ActualizarCliente(cliente);
                    if (esFinTransaccion == 0)
                    {                        
                        return Response<ClienteDto>.Ok(new(), "Ocurrio un error al eliminar el cliente");
                    }
                    else
                    {
                        clienteDto = await ConsultarCliente(clienteRequest.Identificacion);
                        return Response<ClienteDto>.Ok(clienteDto.Data, "Cliente actualizado Correctamente");
                    }
                }
                else
                {
                    return Response<ClienteDto>.Ok(clienteDto.Data, clienteDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClienteDto>.Error("Ocurrio un error al actualizar el cliente");
            }
        }

        public async Task<Response<ClienteDto>> ConsultarCliente(string identificacion)
        {
            try
            {
                Cliente cliente = await _clienteRepo.ConsultarCliente(identificacion);
                if (null == cliente)
                {
                    return Response<ClienteDto>.Ok(new(), "Cliente no encontrado");
                }
                ClienteDto clienteDto = await _mapper.From(cliente).AdaptToTypeAsync<ClienteDto>();
                return Response<ClienteDto>.Ok(clienteDto, "Transaccion procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<ClienteDto>.Error("Ocurrio un error al consultar el cliente");
            }
        }

        public async Task<Response<ClienteDto>> CrearCliente(ClienteDto clienteRequest)
        {
            try
            {
                Response<ClienteDto> clienteDto = await ConsultarCliente(clienteRequest.Identificacion);
                if (string.IsNullOrEmpty(clienteDto.Data.Identificacion))
                {
                    Cliente cliente = await _mapper.From(clienteRequest).AdaptToTypeAsync<Cliente>();
                    int esFinTransaccion = await _clienteRepo.CrearCliente(cliente);
                    if (esFinTransaccion == 0)
                    {
                        _logger.LogWarning("Ocurrio un error al crear el cliente", cliente.Identificacion);
                        return Response<ClienteDto>.Ok(new ClienteDto(), "Ocurrio un error al crear el cliente");
                    }
                    clienteDto = await ConsultarCliente(clienteRequest.Identificacion);
                    return Response<ClienteDto>.Ok(clienteDto.Data, "Cliente Creado Correctamente");
                }
                else
                {
                    return Response<ClienteDto>.Ok(new ClienteDto(), "El cliente ya se encuentra registrado");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClienteDto>.Error("Ocurrio un error al crear el cliente");
            }
        }

        public async Task<Response<int>> EliminarCliente(string identificacion)
        {
            try
            {
                Response<ClienteDto>? clienteDto = await ConsultarCliente(identificacion);
                if (!string.IsNullOrEmpty(clienteDto.Data.Identificacion))
                {
                    Cliente cliente = await _mapper.From(clienteDto.Data).AdaptToTypeAsync<Cliente>();
                    int esFinTransaccion = await _clienteRepo.EliminarCliente(cliente);
                    if (esFinTransaccion == 0)
                    {
                        return Response<int>.Ok(esFinTransaccion, "Ocurrio un error al eliminar el cliente");
                    }
                    else
                    {
                        return Response<int>.Ok(esFinTransaccion, "Cliente eliminado Correctamente");
                    }
                }
                else
                {
                    return Response<int>.Ok(0, clienteDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<int>.Error("Ocurrio un error al eliminar el cliente");
            }
        }
    }
}
