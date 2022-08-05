using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Models;
using CreditoAuto.Entities.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CreditoAuto.Infraestructure.Services
{
    public class EjecutivoService : IEjecutivoService
    {
        private readonly IEjecutivoRepository _ejecutivoRepo;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public EjecutivoService(IMapper mapper, ILogger<EjecutivoService> logger, IEjecutivoRepository ejecutivoRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _ejecutivoRepo = ejecutivoRepo;
        }
      
        public async Task<Response<EjecutivoDto>> Consultar(string identificacion)
        {
            try
            {
                Ejecutivo Ejecutivo = await _ejecutivoRepo.Consultar(identificacion);
                if (null == Ejecutivo)
                {
                    return Response<EjecutivoDto>.Ok(new(), "Ejecutivo no encontrado");
                }
                EjecutivoDto EjecutivoDto = await _mapper.From(Ejecutivo).AdaptToTypeAsync<EjecutivoDto>();
                return Response<EjecutivoDto>.Ok(EjecutivoDto, "Transaccion procesada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrio un error de tipo {0}", ex);
                 return Response<EjecutivoDto>.Error("Ocurrio un error al consultar el Ejecutivo");
            }
        }
    }
}
