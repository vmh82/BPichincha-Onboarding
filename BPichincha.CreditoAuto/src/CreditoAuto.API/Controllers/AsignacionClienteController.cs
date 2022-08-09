using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CreditoAuto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionClienteController : ControllerBase
    {
        private readonly IAsignacionClienteService _AsignacionClienteService;
        public AsignacionClienteController(IAsignacionClienteService AsignacionClienteService)
        {
            _AsignacionClienteService = AsignacionClienteService;
        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientePatioDto))]
        public async Task<IActionResult> Consultar(AsignacionClienteDto clienteDto)
        {
            Response<ClientePatioDto> response = await _AsignacionClienteService.Consultar(clienteDto.Identificacion, clienteDto.NumeroPuntoVenta);
            return StatusCode((int)response.Status, response);
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientePatioDto))]
        public async Task<IActionResult> Crear(AsignacionClienteDto AsignacionClienteDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<ClientePatioDto> response = await _AsignacionClienteService.Crear(AsignacionClienteDto);
                return StatusCode((int)response.Status, response);
               
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> Eliminar(AsignacionClienteDto asignacionClienteDto)
        {

            Response<int> response = await _AsignacionClienteService.Eliminar(asignacionClienteDto.Identificacion, asignacionClienteDto.NumeroPuntoVenta);
            return StatusCode((int)response.Status, response);

        }


        [HttpPut]
        [Route("Actualizar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientePatioDto))]
        public async Task<IActionResult> Actualizar(AsignacionClienteDto AsignacionClienteDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<ClientePatioDto> response = await _AsignacionClienteService.Actualizar(AsignacionClienteDto);
                return StatusCode((int)response.Status, response);

            }
        }
    }
}
