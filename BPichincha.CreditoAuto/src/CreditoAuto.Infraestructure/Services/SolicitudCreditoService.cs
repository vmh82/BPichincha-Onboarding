using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class SolicitudCreditoService : ISolicitudCreditoService
    {
        private readonly ISolicitudCreditoRepository _solicitudRepo;
        private readonly ILogger _logger;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        private readonly IPatioService _patioService;
        private readonly IEjecutivoService _ejecutivoService;
        private readonly IAsignacionClienteService _asignacionClienteService;
        private readonly IVehiculoService _vehiculoService;
        public SolicitudCreditoService(IMapper mapper, 
            ILogger<SolicitudCreditoService> logger,
            
            ISolicitudCreditoRepository solicitudRepo, IClienteService clienteService,
            IPatioService patioService, IEjecutivoService ejecutivoService,
            IAsignacionClienteService asignacionClienteService,
            IVehiculoService vehiculoService)
        {
            _mapper = mapper;
            _logger = logger;
            _clienteService = clienteService;
            _solicitudRepo = solicitudRepo;
            _patioService = patioService;
            _ejecutivoService = ejecutivoService;
            _asignacionClienteService = asignacionClienteService;
            _vehiculoService = vehiculoService;
        }
       
        public async Task<Response<SolicitudCreditoDto>> Consultar(string identificacion)
        {
            try
            {
                Response<ClienteDto> clienteResponse = await _clienteService.ConsultarCliente(identificacion);
                if (string.IsNullOrEmpty(clienteResponse.Data.Identificacion))
                {
                    return Response<SolicitudCreditoDto>.Ok(new(), clienteResponse.Mensaje);
                }
                SolicitudCredito credito = await _solicitudRepo.Consultar(identificacion);
                SolicitudCreditoDto creditoDto =  await _mapper.From(credito).AdaptToTypeAsync<SolicitudCreditoDto>();
                return Response<SolicitudCreditoDto>.Ok(creditoDto, "Transaccion Procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<SolicitudCreditoDto>.Error("Ocurrio un error al consultar el SolicitudCredito");
            }
        }

        public async Task<Response<SolicitudCreditoDto>> Crear(SolicitudCreditoDto solicitudCreditoRequest)
        {
            try
            {
                Response<bool>? EsSolicitudValida = await ValidarSolicitud(solicitudCreditoRequest);
                if (!EsSolicitudValida.Data)
                {
                    return Response<SolicitudCreditoDto>.Ok(new(), EsSolicitudValida.Mensaje);
                }
                SolicitudCredito solicitud = await _mapper.From(solicitudCreditoRequest).AdaptToTypeAsync<SolicitudCredito>();
                SolicitudCredito solicitudPorDia =  await _solicitudRepo.ValidarSolicitudPorDia(solicitud);
                if(null != solicitudPorDia)
                {
                    return Response<SolicitudCreditoDto>.Ok(new(), "El cliente ya cuenta con una solicitud en proceso");
                }
                SolicitudCredito reservaVehiculo = await _solicitudRepo.ValidarReservaVehiculo(solicitud);
                if(null != reservaVehiculo)
                {
                    return Response<SolicitudCreditoDto>.Ok(new(), "El vehiculo ya se encuentra reservado");
                }
                int esFinTransaccion =  await _solicitudRepo.Crear(solicitud);
                AsignacionClienteDto? asginacionClienteDto = await _mapper.From(solicitudCreditoRequest).AdaptToTypeAsync<AsignacionClienteDto>();
                await _asignacionClienteService.Crear(asginacionClienteDto);
                Response<SolicitudCreditoDto>? solicitudResponse = await Consultar(solicitudCreditoRequest.IdentificacionCliente);
                return Response<SolicitudCreditoDto>.Ok(solicitudResponse.Data, "Solicitud creada exitosamente");
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<SolicitudCreditoDto>.Error("Ocurrio un error al crear el SolicitudCredito");
            }
        }

        public async Task<Response<bool>> ValidarSolicitud(SolicitudCreditoDto solicitudCreditoRequest)
        {
            Response<ClienteDto>? clienteResponse = await _clienteService.ConsultarCliente(solicitudCreditoRequest.IdentificacionCliente);
            Response<PatioDto>? patioResponse = await _patioService.Consultar(solicitudCreditoRequest.NumeroPuntoVenta);
            Response<EjecutivoDto> ejecutivoResponse = await _ejecutivoService.Consultar(solicitudCreditoRequest.IdentificacionEjecutivo);
            Response<VehiculoDto> vehiculoResponse = await _vehiculoService.Consultar(solicitudCreditoRequest.Placa);
            if (string.IsNullOrEmpty(clienteResponse.Data.Identificacion))
            {
                return Response<bool>.Ok(false, clienteResponse.Mensaje);
            }

            if (string.IsNullOrEmpty(patioResponse.Data.Nombre))
            {
                return Response<bool>.Ok(false, patioResponse.Mensaje);
            }

            if (string.IsNullOrEmpty(ejecutivoResponse.Data.Nombres))
            {
                return Response<bool>.Ok(false, ejecutivoResponse.Mensaje);
            }

            if (string.IsNullOrEmpty(vehiculoResponse.Data.NumeroChasis))
            {
                return Response<bool>.Ok(false, vehiculoResponse.Mensaje);
            }

            return  Response<bool>.Ok(true, string.Empty);
        }
    }
}
