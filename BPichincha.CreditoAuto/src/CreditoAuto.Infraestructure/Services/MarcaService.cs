using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _MarcaRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public MarcaService(IMapper mapper, ILogger<MarcaService> logger,IMarcaRepository MarcaRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _MarcaRepo = MarcaRepo;
        }
        public async Task<Response<MarcaDto>> Actualizar(MarcaDto MarcaRequest)
        {
            try
            {
                Response<MarcaDto>? MarcaDto = await Consultar(MarcaRequest.MarcaId);
                if (!string.IsNullOrEmpty(MarcaDto.Data.Descripcion))
                {
                    Marca marca = await _mapper.From(MarcaRequest).AdaptToTypeAsync<Marca>();
                    int esFinTransaccion = await _MarcaRepo.Actualizar(marca);
                    if (esFinTransaccion == 0)
                    {                        
                        return Response<MarcaDto>.Ok(new(), "Ocurrio un error al eliminar el Marca");
                    }
                    else
                    {
                        MarcaDto = await Consultar(MarcaRequest.MarcaId);
                        return Response<MarcaDto>.Ok(MarcaDto.Data, "Marca actualizado Correctamente");
                    }
                }
                else
                {
                    return Response<MarcaDto>.Ok(MarcaDto.Data, MarcaDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<MarcaDto>.Error("Ocurrio un error al actualizar el Marca");
            }
        }

        public async Task<Response<MarcaDto>> Consultar(int marcaId)
        {
            try
            {
                Marca Marca = await _MarcaRepo.Consultar(marcaId);
                if (null == Marca)
                {
                    return Response<MarcaDto>.Ok(new(), "Marca no encontrado");
                }
                MarcaDto MarcaDto = await _mapper.From(Marca).AdaptToTypeAsync<MarcaDto>();
                return Response<MarcaDto>.Ok(MarcaDto, "Transaccion procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<MarcaDto>.Error("Ocurrio un error al consultar el Marca");
            }
        }

        public async Task<Response<MarcaDto>> Crear(MarcaDto MarcaRequest)
        {
            try
            {
                Response<MarcaDto> MarcaDto = await Consultar(MarcaRequest.MarcaId);
                if (string.IsNullOrEmpty(MarcaDto.Data.Descripcion))
                {
                    Marca marca = await _mapper.From(MarcaRequest).AdaptToTypeAsync<Marca>();
                    int esFinTransaccion = await _MarcaRepo.Crear(marca);
                    if (esFinTransaccion == 0)
                    {
                        _logger.LogWarning("Ocurrio un error al crear el Marca", marca.Descripcion);
                        return Response<MarcaDto>.Ok(new MarcaDto(), "Ocurrio un error al crear el Marca");
                    }
                    MarcaDto = await Consultar(MarcaRequest.MarcaId);
                    return Response<MarcaDto>.Ok(MarcaDto.Data, "Marca Creado Correctamente");
                }
                else
                {
                    return Response<MarcaDto>.Ok(new MarcaDto(), "El Marca ya se encuentra registrado");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<MarcaDto>.Error("Ocurrio un error al crear el Marca");
            }
        }

        public async Task<Response<int>> Eliminar(int marcaId)
        {
            try
            {
                Response<MarcaDto>? MarcaDto = await Consultar(marcaId);
                if (!string.IsNullOrEmpty(MarcaDto.Data.Descripcion))
                {
                    Marca Marca = await _mapper.From(MarcaDto.Data).AdaptToTypeAsync<Marca>();
                    int esFinTransaccion = await _MarcaRepo.Eliminar(Marca);
                    if (esFinTransaccion == 0)
                    {
                        return Response<int>.Ok(esFinTransaccion, "Ocurrio un error al eliminar el Marca");
                    }
                    else
                    {
                        return Response<int>.Ok(esFinTransaccion, "Marca eliminado Correctamente");
                    }
                }
                else
                {
                    return Response<int>.Ok(0, MarcaDto.Mensaje);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                return Response<int>.Error("Ocurrio un error al eliminar el Marca");
            }
        }
    }
}
