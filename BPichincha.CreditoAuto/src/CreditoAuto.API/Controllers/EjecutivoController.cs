using CreditoAuto.Domain.Interfaces;
using CreditoAuto.Entities.Dto;
using CreditoAuto.Entities.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CreditoAuto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjecutivoController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public EjecutivoController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("Consultar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        public async Task<IActionResult> Consultar(string identificacion)
        {
            Response<ClienteDto> response = await _clienteService.ConsultarCliente(identificacion);
            return StatusCode((int)response.Status, response);
        }

        [HttpPost]
        [Route("Crear")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        public async Task<IActionResult> Crear(ClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<ClienteDto> response = await _clienteService.CrearCliente(clienteDto);
                return StatusCode((int)response.Status, response);
               
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> Eliminar(string identificacion)
        {

            Response<int> response = await _clienteService.EliminarCliente(identificacion);
            return StatusCode((int)response.Status, response);

        }


        [HttpPut]
        [Route("Actualizar")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        public async Task<IActionResult> Actualizar(ClienteDto clienteDto)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                Response<ClienteDto> response = await _clienteService.ActualizarCliente(clienteDto);
                return StatusCode((int)response.Status, response);

            }
        }
    }
}
