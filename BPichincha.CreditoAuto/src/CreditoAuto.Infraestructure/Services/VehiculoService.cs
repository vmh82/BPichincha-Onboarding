using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public VehiculoService(IMapper mapper, ILogger<VehiculoService> logger, IVehiculoRepository vehiculoRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _vehiculoRepo = vehiculoRepo;
        }
        public async Task<Response<VehiculoDto>> Actualizar(VehiculoDto VehiculoRequest)
        {
            try
            {
                Response<VehiculoDto>? VehiculoDto = await Consultar(VehiculoRequest.Placa);
                if (!string.IsNullOrEmpty(VehiculoDto.Data.Placa))
                {
                    Vehiculo Vehiculo = await _mapper.From(VehiculoRequest).AdaptToTypeAsync<Vehiculo>();
                    int esFinTransaccion = await _vehiculoRepo.Actualizar(Vehiculo);
                    if (esFinTransaccion == 0)
                    {                        
                        return Response<VehiculoDto>.Ok(new(), "Ocurrio un error al eliminar el Vehiculo");
                    }
                    else
                    {
                        VehiculoDto = await Consultar(VehiculoRequest.Placa);
                        return Response<VehiculoDto>.Ok(VehiculoDto.Data, "Vehiculo actualizado Correctamente");
                    }
                }
                else
                {
                    return Response<VehiculoDto>.Ok(VehiculoDto.Data, VehiculoDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<VehiculoDto>.Error("Ocurrio un error al actualizar el Vehiculo");
            }
        }

        public async Task<Response<VehiculoDto>> Consultar(string placa)
        {
            try
            {
                Vehiculo Vehiculo = await _vehiculoRepo.Consultar(placa);
                if (null == Vehiculo)
                {
                    return Response<VehiculoDto>.Ok(new(), "Vehiculo no encontrado");
                }
                VehiculoDto VehiculoDto = await _mapper.From(Vehiculo).AdaptToTypeAsync<VehiculoDto>();
                return Response<VehiculoDto>.Ok(VehiculoDto, "Transaccion procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<VehiculoDto>.Error("Ocurrio un error al consultar el Vehiculo");
            }
        }

        public async Task<Response<VehiculoDto>> Crear(VehiculoDto vehiculoRequest)
        {
            try
            {
                Response<VehiculoDto> VehiculoDto = await Consultar(vehiculoRequest.Placa);
                if (string.IsNullOrEmpty(VehiculoDto.Data.Placa))
                {
                    Vehiculo Vehiculo = await _mapper.From(vehiculoRequest).AdaptToTypeAsync<Vehiculo>();
                    int esFinTransaccion = await _vehiculoRepo.Crear(Vehiculo);
                    if (esFinTransaccion == 0)
                    {
                        _logger.LogWarning("Ocurrio un error al crear el Vehiculo", Vehiculo.Placa);
                        return Response<VehiculoDto>.Ok(new VehiculoDto(), "Ocurrio un error al crear el Vehiculo");
                    }
                    VehiculoDto = await Consultar(vehiculoRequest.Placa);
                    return Response<VehiculoDto>.Ok(VehiculoDto.Data, "Vehiculo Creado Correctamente");
                }
                else
                {
                    return Response<VehiculoDto>.Ok(new VehiculoDto(), "El Vehiculo ya se encuentra registrado");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<VehiculoDto>.Error("Ocurrio un error al crear el Vehiculo");
            }
        }

        public async Task<Response<int>> Eliminar(string placa)
        {
            try
            {
                Response<VehiculoDto>? VehiculoDto = await Consultar(placa);
                if (!string.IsNullOrEmpty(VehiculoDto.Data.Placa))
                {
                    Vehiculo Vehiculo = await _mapper.From(VehiculoDto.Data).AdaptToTypeAsync<Vehiculo>();
                    int esFinTransaccion = await _vehiculoRepo.Eliminar(Vehiculo);
                    if (esFinTransaccion == 0)
                    {
                        return Response<int>.Ok(esFinTransaccion, "Ocurrio un error al eliminar el Vehiculo");
                    }
                    else
                    {
                        return Response<int>.Ok(esFinTransaccion, "Vehiculo eliminado Correctamente");
                    }
                }
                else
                {
                    return Response<int>.Ok(0, VehiculoDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<int>.Error("Ocurrio un error al eliminar el Vehiculo");
            }
        }
    }
}
