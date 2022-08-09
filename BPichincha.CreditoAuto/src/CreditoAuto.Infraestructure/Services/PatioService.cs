using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class PatioService : IPatioService
    {
        private readonly IPatioRepository _patioRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public PatioService(IMapper mapper, ILogger<PatioService> logger,IPatioRepository PatioRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _patioRepo = PatioRepo;
        }
        public async Task<Response<PatioDto>> Actualizar(PatioDto PatioRequest)
        {
            try
            {
                Response<PatioDto>? PatioDto = await Consultar(PatioRequest.NumeroPuntoVenta);
                if (!string.IsNullOrEmpty(PatioDto.Data.Nombre))
                {
                    Patio Patio = await _mapper.From(PatioRequest).AdaptToTypeAsync<Patio>();
                    int esFinTransaccion = await _patioRepo.Actualizar(Patio);
                    if (esFinTransaccion == 0)
                    {                        
                        return Response<PatioDto>.Ok(new(), "Ocurrio un error al eliminar el Patio");
                    }
                    else
                    {
                        PatioDto = await Consultar(PatioRequest.NumeroPuntoVenta);
                        return Response<PatioDto>.Ok(PatioDto.Data, "Patio actualizado Correctamente");
                    }
                }
                else
                {
                    return Response<PatioDto>.Ok(PatioDto.Data, PatioDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<PatioDto>.Error("Ocurrio un error al actualizar el Patio");
            }
        }

        public async Task<Response<PatioDto>> Consultar(int numeroPuntoVenta)
        {
            try
            {
                Patio Patio = await _patioRepo.Consultar(numeroPuntoVenta);
                if (null == Patio)
                {
                    return Response<PatioDto>.Ok(new(), "Patio no encontrado");
                }
                PatioDto PatioDto = await _mapper.From(Patio).AdaptToTypeAsync<PatioDto>();
                return Response<PatioDto>.Ok(PatioDto, "Transaccion procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<PatioDto>.Error("Ocurrio un error al consultar el Patio");
            }
        }

        public async Task<Response<PatioDto>> Crear(PatioDto PatioRequest)
        {
            try
            {
                Response<PatioDto> PatioDto = await Consultar(PatioRequest.NumeroPuntoVenta);
                if (string.IsNullOrEmpty(PatioDto.Data.Nombre))
                {
                    Patio Patio = await _mapper.From(PatioRequest).AdaptToTypeAsync<Patio>();
                    int esFinTransaccion = await _patioRepo.Crear(Patio);
                    if (esFinTransaccion == 0)
                    {
                        _logger.LogWarning("Ocurrio un error al crear el Patio", Patio.NumeroPuntoVenta);
                        return Response<PatioDto>.Ok(new PatioDto(), "Ocurrio un error al crear el Patio");
                    }
                    PatioDto = await Consultar(PatioRequest.NumeroPuntoVenta);
                    return Response<PatioDto>.Ok(PatioDto.Data, "Patio Creado Correctamente");
                }
                else
                {
                    return Response<PatioDto>.Ok(new PatioDto(), "El Patio ya se encuentra registrado");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<PatioDto>.Error("Ocurrio un error al crear el Patio");
            }
        }

        public async Task<Response<int>> Eliminar(int numeroPuntoVenta)
        {
            try
            {

                Patio patio = await _patioRepo.Consultar(numeroPuntoVenta);
                if (null != patio && (patio.SolicitudCreditos.Count() > 0 || patio.AsignacionClientes.Count() > 0))
                {
                    return Response<int>.Error("No se puede eliminar el patio, existe una solicitud o asignacion asociada");
                }
                if (null == patio)
                {
                    return Response<int>.Ok(new(), "Patio no encontrado");
                }
                int esFinTransaccion = await _patioRepo.Eliminar(patio);
                return Response<int>.Ok(esFinTransaccion, "patio eliminado correctamente");
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<int>.Error("Ocurrio un error al eliminar el Patio");
            }
        }
    }
}
