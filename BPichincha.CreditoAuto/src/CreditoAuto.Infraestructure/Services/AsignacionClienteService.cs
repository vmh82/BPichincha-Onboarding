using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class AsignacionClienteService : IAsignacionClienteService
    {
        private readonly IAsignacionClienteRepository _asignacionRepo;
        private readonly IClienteService _clienteService;
        private readonly IPatioService _patioService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public AsignacionClienteService(IMapper mapper, ILogger<AsignacionClienteService> logger,IAsignacionClienteRepository asignacionRepo, IClienteService clienteService, IPatioService patioService)
        {
            _mapper = mapper;
            _logger = logger;
            _asignacionRepo = asignacionRepo;
            _clienteService = clienteService;
            _patioService = patioService;
        }
        public async Task<Response<AsignacionClienteDto>> Actualizar(AsignacionClienteDto clienteRequest)
        {
            try
            {
                Response<ClientePatioDto>? AsignacionClienteDto = await Consultar(clienteRequest.Identificacion);
                if (!string.IsNullOrEmpty(AsignacionClienteDto.Data.Identificacion))
                {
                    return Response<AsignacionClienteDto>.Ok(new(), "Ocurrio un error al actualizar la asignacion");
                }
                else
                {
                    return Response<AsignacionClienteDto>.Ok(new(), AsignacionClienteDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<AsignacionClienteDto>.Error("Ocurrio un error al actualizar el cliente");
            }
        }

        public async Task<Response<ClientePatioDto>> Consultar(string identificacion)
        {
            try
            {
                AsignacionCliente cliente = await _asignacionRepo.Consultar(identificacion);
                if (null == cliente)
                {
                    return Response<ClientePatioDto>.Ok(new(), "Cliente no encontrado");
                }
                ClientePatioDto clientePatio = await _mapper.From(cliente).AdaptToTypeAsync<ClientePatioDto>();
                return Response<ClientePatioDto>.Ok(clientePatio, "Transaccion procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<ClientePatioDto>.Error("Ocurrio un error al consultar el cliente");
            }
        }

        public async Task<Response<ClientePatioDto>> Crear(AsignacionClienteDto asignacionRequest)
        {
            try
            {
                Response<ClienteDto> clienteDto = await _clienteService.ConsultarCliente(asignacionRequest.Identificacion);
                Response<PatioDto> patioDto = await _patioService.Consultar(asignacionRequest.NumeroPuntoVenta);

                if (string.IsNullOrEmpty(clienteDto.Data.Identificacion))
                {
                  
                    return Response<ClientePatioDto>.Ok(new(), clienteDto.Mensaje);
                }

                if (string.IsNullOrEmpty(patioDto.Data.Nombre))
                {
                    return Response<ClientePatioDto>.Ok(new(), patioDto.Mensaje);
                }

                if (string.IsNullOrEmpty(patioDto.Data.Nombre))
                {
                    return Response<ClientePatioDto>.Ok(new(), patioDto.Mensaje);
                }
                Response<ClientePatioDto> clienteAsignacion = await Consultar(asignacionRequest.Identificacion);
                if (!string.IsNullOrEmpty(clienteAsignacion.Data.Identificacion))
                {
                    return Response<ClientePatioDto>.Ok(new(), "El cliente ya se encuentra asignado");
                }
                AsignacionCliente asignacionCliente = await _mapper.From(asignacionRequest).AdaptToTypeAsync<AsignacionCliente>();
                int esFinTransaccion = await _asignacionRepo.Crear(asignacionCliente);
                Response<ClientePatioDto>? clientePatio = await Consultar(asignacionRequest.Identificacion);
                return Response<ClientePatioDto>.Ok(clientePatio.Data, "Cliente asignado correctamente");
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClientePatioDto>.Error("Ocurrio un error al asignar el cliente");
            }
        }

        public async Task<Response<int>> Eliminar(string identificacion)
        {
            try
            {
                Response<ClientePatioDto>? AsignacionClienteDto = await Consultar(identificacion);
                if (!string.IsNullOrEmpty(AsignacionClienteDto.Data.Identificacion))
                {
                    return Response<int>.Ok(1, "Cliente eliminado Correctamente");
                }
                else
                {
                    return Response<int>.Ok(0, AsignacionClienteDto.Mensaje);
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
