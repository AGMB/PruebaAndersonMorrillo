using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BancoPichincha.API.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Endpoint para obtener todos los clientes
        /// </summary>
        [HttpGet]
        [Route("obtener-clientes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _clienteService.ObtenerClientesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint para obtener un cliente en especifico
        /// </summary>
        [HttpGet]
        [Route("obtener-cliente/{clienteid}")]
        public async Task<IActionResult> ObtenerCliente(int clienteid)
        {
            var result =  await _clienteService.ObtenerClienteAsync(clienteid);
            if(result is null)
                return NotFound($"No se encontro el cliente {clienteid}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para agregar un cliente
        /// </summary>
        [HttpPost]
        [Route("agregar-cliente")]
        public async Task<IActionResult> AgregarCliente(ClienteAddDto cliente)
        {
            var resultado = await _clienteService.GuardarClienteAsync(cliente);
            if(!resultado)
            {
                return BadRequest();
            }
            return Created("agregar-cliente",cliente);
        }

        /// <summary>
        /// Endpoint para modificar un cliente
        /// </summary>
        [HttpPut]
        [Route("modificar-cliente")]
        public async Task<IActionResult> ModificarCliente(ClienteDto cliente)
        { 
            var resultado = await _clienteService.ActualizarClienteAsync(cliente);
            if (!resultado)
            {
                return NotFound($"No existe el cliente con el id: {cliente.ClienteID}");
            }
            return Ok();
        }

        /// <summary>
        /// Endpoint para eliminar un cliente
        /// </summary>
        [HttpDelete]
        [Route("eliminar-cliente/{clienteid}")]
        public async Task<IActionResult> EliminarCliente(int clienteid)
        {
            var resultado = await _clienteService.EliminarClienteAsync(clienteid);
            if (!resultado)
            {
                return NotFound($"No existe el cliente con el id: {clienteid}");
            }
            return Ok();
        }
    }
}
