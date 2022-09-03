using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BancoPichincha.API.Controllers
{
    [ApiController]
    [Route("movimientos")]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;
        private readonly ICuentaService _cuentaService;

        public MovimientoController(IMovimientoService movimientoService, ICuentaService cuentaService)
        {
            _movimientoService = movimientoService;
            _cuentaService = cuentaService;
        }

        /// <summary>
        /// Endpoint para obtener todos los movimientos por fecha en orden desc 
        /// </summary>
        [HttpGet]
        [Route("obtener-movimientos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarMovimientos()
        {
            var result = await _movimientoService.ObtenerMovimientosAsync();

            return Ok(result);
        }

        /// <summary>
        /// Endpoint para obtener todos los movimientos por cuenta
        /// </summary>
        [HttpGet]
        [Route("obtener-movimientos/{numerocuenta}")]
        public async Task<IActionResult> ObtenerCliente(string numerocuenta)
        {
            var result = await _movimientoService.ObtenerMovimientosPorCuentaAsync(numerocuenta);
            if (result is null)
                return NotFound($"No existen movimientos para la cuenta {numerocuenta}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para obtener movimiento por id
        /// </summary>
        [HttpGet]
        [Route("obtener-movimiento/{movimientoid}")]
        public async Task<IActionResult> ObtenerMovimiento(int movimientoid)
        {
            var result = await _movimientoService.ObtenerMovimientoPorIdAsync(movimientoid);
            if (result is null)
                return NotFound($"No existe la transaccion # {movimientoid}");
            return Ok(result);
        }

        /// <summary>
        /// Endpoint para modificar un movimiento
        /// </summary>
        [HttpPut]
        [Route("modificar-movimiento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModificarMovimiento(MovimientoDto movimiento)
        {
            var resultado = await _movimientoService.ActualizarMovimientoAsync(movimiento);
            if (!resultado)
            {
                return NotFound($"No existe la transaccion #: {movimiento.MovimientoId}");
            }
            return Ok();
        }

        /// <summary>
        /// Endpoint para realizar un movimiento
        /// </summary>
        [HttpPost]
        [Route("realizar-movimiento")]
        public async Task<ActionResult<ResponseModel>> AgregarMovimiento(MovimientoRegistroDto movimiento)
        {
            var result = await _cuentaService.ObtenerCuentaPorNumeroCuentaAsync(movimiento.NumeroCuenta);
            if (result is null)
                return NotFound(new ResponseModel { IsSuccess=false,StatusCode=404,Mensaje=$"No existe la cuenta {movimiento.NumeroCuenta}"});

            var resultado = await _movimientoService.RegistrarMovimientoSaldosAsync(movimiento);

            return Created("realizar-movimiento",resultado);
        }

        /// <summary>
        /// Endpoint para eliminar un movimiento por id
        /// </summary>
        [HttpDelete]
        [Route("eliminar-movimiento/{movimientoid}")]
        public async Task<IActionResult> EliminarCliente(int movimientoid)
        {
            var resultado = await _movimientoService.EliminarMovimientoAsync(movimientoid);
            if (!resultado)
            {
                return NotFound($"No existe la transaccion #: {movimientoid}");
            }
            return Ok();
        }

        [HttpGet]
        [Route("generar-reporte/{identificacion}&{fechainicio}&{fechafin}")]
        public async Task<IActionResult> GenerarReporteMovimientos(string identificacion, DateTime fechainicio, DateTime fechafin)
        {
            var resultado = await _movimientoService.GenerarReportePorClienteYRangoFechasAsync(identificacion, fechainicio, fechafin);

            return Ok(resultado);
        }
    }
}
