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
        public async Task<Response<ClientePatioDto>> Actualizar(AsignacionClienteDto clienteRequest)
        {
            try
            {
                Response<ClientePatioDto>? asignacionClienteDto = await Consultar(clienteRequest.Identificacion, clienteRequest.NumeroPuntoVenta);
                if (!string.IsNullOrEmpty(asignacionClienteDto.Data.Identificacion))
                {
                    clienteRequest.FechaAsignacion = clienteRequest.FechaAsignacion ?? DateTime.Now;
                    AsignacionCliente asignacionCliente = await _mapper.From(clienteRequest).AdaptToTypeAsync<AsignacionCliente>();
                    asignacionCliente.AsignacionId = asignacionClienteDto.Data.AsignacionId;
                    int esFinTransaccion = await _asignacionRepo.Actualizar(asignacionCliente);
                    if (esFinTransaccion == 0)
                    {
                        return Response<ClientePatioDto>.Ok(new(), "Ocurrio un error al actualizar la asignacion");
                    }
                    else
                    {
                        asignacionClienteDto = await Consultar(clienteRequest.Identificacion, clienteRequest.NumeroPuntoVenta);
                        return Response<ClientePatioDto>.Ok(asignacionClienteDto.Data, "asignacion actualizada Correctamente");
                    }
                }
                else
                {
                    return Response<ClientePatioDto>.Ok(new(), asignacionClienteDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClientePatioDto>.Error("Ocurrio un error al actualizar la asignacion");
            }
        }

        public async Task<Response<ClientePatioDto>> Consultar(string identificacion, int numeroPuntoVenta)
        {
            try
            {
                AsignacionCliente cliente = await _asignacionRepo.Consultar(identificacion, numeroPuntoVenta);
                if (null == cliente)
                {
                    return Response<ClientePatioDto>.Ok(new(), "Asignacion no encontrada");
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

                Response<ClientePatioDto> clienteAsignacion = await Consultar(asignacionRequest.Identificacion, asignacionRequest.NumeroPuntoVenta);
                if (!string.IsNullOrEmpty(clienteAsignacion.Data.Identificacion))
                {
                    return Response<ClientePatioDto>.Ok(new(), "El cliente ya se encuentra asignado");
                }
                AsignacionCliente asignacionCliente = await _mapper.From(asignacionRequest).AdaptToTypeAsync<AsignacionCliente>();
                int esFinTransaccion = await _asignacionRepo.Crear(asignacionCliente);
                Response<ClientePatioDto>? clientePatio = await Consultar(asignacionRequest.Identificacion, asignacionRequest.NumeroPuntoVenta);
                return Response<ClientePatioDto>.Ok(clientePatio.Data, "Cliente asignado correctamente");
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<ClientePatioDto>.Error("Ocurrio un error al asignar el cliente");
            }
        }

        public async Task<Response<int>> Eliminar(string identificacion, int numeroPuntoVenta)
        {
            try
            {


                Response<ClienteDto> clienteDto = await _clienteService.ConsultarCliente(identificacion);
                Response<PatioDto> patioDto = await _patioService.Consultar(numeroPuntoVenta);

                if (string.IsNullOrEmpty(clienteDto.Data.Identificacion))
                {

                    return Response<int>.Ok(new(), clienteDto.Mensaje);
                }

                if (string.IsNullOrEmpty(patioDto.Data.Nombre))
                {
                    return Response<int>.Ok(new(), patioDto.Mensaje);
                }

                Response<ClientePatioDto>? AsignacionClienteDto = await Consultar(identificacion, numeroPuntoVenta);

                if (!string.IsNullOrEmpty(AsignacionClienteDto.Data.Identificacion))
                {

                    AsignacionCliente asignacion = await _asignacionRepo.Consultar(identificacion, numeroPuntoVenta);
                    int esFinTransaccion = await _asignacionRepo.Eliminar(asignacion);
                    return Response<int>.Ok(esFinTransaccion, "asignacion eliminada correctamente");
                }
                else
                {
                    return Response<int>.Ok(0, AsignacionClienteDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<int>.Error("Ocurrio un error al eliminar la asigacion");
            }
        }
    }
}
