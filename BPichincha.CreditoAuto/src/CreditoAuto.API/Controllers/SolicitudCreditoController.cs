using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CreditoAuto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCreditoController : ControllerBase
    {
        private readonly ISolicitudCreditoService _SolicitudCreditoService;
        public SolicitudCreditoController(ISolicitudCreditoService SolicitudCreditoService)
        {
            _SolicitudCreditoService = SolicitudCreditoService;
        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SolicitudCreditoDto))]
        public async Task<IActionResult> Consultar(string identificacion)
        {
            Response<SolicitudCreditoDto> response = await _SolicitudCreditoService.Consultar(identificacion);
            return StatusCode((int)response.Status, response);
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SolicitudCreditoDto))]
        public async Task<IActionResult> Crear(SolicitudCreditoDto SolicitudCreditoDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<SolicitudCreditoDto> response = await _SolicitudCreditoService.Crear(SolicitudCreditoDto);
                return StatusCode((int)response.Status, response);
               
            }
        }
    }
}
