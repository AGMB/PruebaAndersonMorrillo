using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BancoPichincha.API.Controllers
{
    [ApiController]
    [Route("cuentas")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        private readonly IClienteService _clienteService;

        public CuentaController(ICuentaService cuentaService, IClienteService clienteService)
        {
            _cuentaService = cuentaService;
            _clienteService = clienteService;
        }

        /// <summary>
        /// Endpoint para obtener todas las cuentas
        /// </summary>
        [HttpGet]
        [Route("obtener-cuentas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarCuentas()
        {
            var result = await _cuentaService.ObtenerCuentasAsync();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint para obtener cuenta por # de cuenta
        /// </summary>
        [HttpGet]
        [Route("obtener-cuenta/{numerocuenta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerCuenta(string numerocuenta)
        {
            var result = await _cuentaService.ObtenerCuentaPorNumeroCuentaAsync(numerocuenta);
            if(result is null)
                return NotFound($"No existe la cuenta {numerocuenta}");
            return Ok(result);
        }

        [HttpGet]
        [Route("obtener-cuentas/{clienteid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarCuentasPorCliente(int clienteid)
        {
            var result = await _cuentaService.ObtenerCuentasPorClienteAsync(clienteid);
            if (result.Count == 0)
                return NotFound($"No existen cuentas para el cliente");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para agregar una cuenta
        /// </summary>
        [HttpPost]
        [Route("agregar-cuenta")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AgregarCuenta(CuentaDto cuenta)
        {
            if (!await _clienteService.ExisteClienteAsync(cuenta.ClienteId))
                return NotFound($"No se puede crear la cuenta, cliente con id {cuenta.ClienteId} NO EXISTE");

            var resultado = await _cuentaService.GuardarCuentaAsync(cuenta);
            if (!resultado)
            {
                return BadRequest();
            }
            return Created("agregar-cuenta", cuenta);
        }

        /// <summary>
        /// Endpoint para modificar una cuenta
        /// </summary>
        [HttpPut]
        [Route("modificar-cuenta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarCuenta(CuentaDto cuenta)
        {
            var resultado = await _cuentaService.ActualizarCuentaAsync(cuenta);
            if (!resultado)
            {
                return NotFound($"No existe la cuenta: {cuenta.NumeroCuenta}");
            }
            return Ok();
        }

        /// <summary>
        /// Endpoint para eliminar una cuenta
        /// </summary>
        [HttpDelete]
        [Route("eliminar-cuenta/{numerocuenta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EliminarCliente(string numerocuenta)
        {
            var resultado = await _cuentaService.EliminarCuentaAsync(numerocuenta);
            if (!resultado)
            {
                return NotFound($"No existe cuenta: {numerocuenta}");
            }
            return Ok();
        }
    }
}
