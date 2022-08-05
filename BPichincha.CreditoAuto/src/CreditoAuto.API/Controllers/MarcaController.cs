using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CreditoAuto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _MarcaService;
        public MarcaController(IMarcaService MarcaService)
        {
            _MarcaService = MarcaService;
        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MarcaDto))]
        public async Task<IActionResult> Consultar(int marcaId)
        {
            Response<MarcaDto> response = await _MarcaService.Consultar(marcaId);
            return StatusCode((int)response.Status, response);
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MarcaDto))]
        public async Task<IActionResult> Crear(MarcaDto MarcaDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<MarcaDto> response = await _MarcaService.Crear(MarcaDto);
                return StatusCode((int)response.Status, response);
               
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> Eliminar(int marcaId)
        {

            Response<int> response = await _MarcaService.Eliminar(marcaId);
            return StatusCode((int)response.Status, response);

        }


        [HttpPut]
        [Route("Actualizar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MarcaDto))]
        public async Task<IActionResult> Actualizar(MarcaDto MarcaDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<MarcaDto> response = await _MarcaService.Actualizar(MarcaDto);
                return StatusCode((int)response.Status, response);

            }
        }
    }
}
