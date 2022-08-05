using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CreditoAuto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _VehiculoService;
        public VehiculoController(IVehiculoService VehiculoService)
        {
            _VehiculoService = VehiculoService;
        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehiculoDto))]
        public async Task<IActionResult> Consultar(string placa)
        {
            Response<VehiculoDto> response = await _VehiculoService.Consultar(placa);
            return StatusCode((int)response.Status, response);
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehiculoDto))]
        public async Task<IActionResult> Crear(VehiculoDto VehiculoDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<VehiculoDto> response = await _VehiculoService.Crear(VehiculoDto);
                return StatusCode((int)response.Status, response);
               
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> Eliminar(string identificacion)
        {

            Response<int> response = await _VehiculoService.Eliminar(identificacion);
            return StatusCode((int)response.Status, response);

        }


        [HttpPut]
        [Route("Actualizar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VehiculoDto))]
        public async Task<IActionResult> Actualizar(VehiculoDto VehiculoDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<VehiculoDto> response = await _VehiculoService.Actualizar(VehiculoDto);
                return StatusCode((int)response.Status, response);

            }
        }
    }
}
