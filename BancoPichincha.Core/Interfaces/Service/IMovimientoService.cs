using BancoPichincha.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Interfaces.Service
{
    public interface IMovimientoService
    {
        Task<IList<MovimientoDto>> ObtenerMovimientosAsync();
        Task<bool> ActualizarMovimientoAsync(MovimientoDto movimiento);
        Task<MovimientoDto> ObtenerMovimientoPorIdAsync(int movimientoId);
        Task<IList<MovimientoDto>> ObtenerMovimientosPorCuentaAsync(string numeroCuenta);
        Task<ResponseModel> RegistrarMovimientoSaldosAsync(MovimientoRegistroDto movimiento);
        Task<bool> EliminarMovimientoAsync(int movimientoid);
        Task<IList<ReporteDto>> GenerarReportePorClienteYRangoFechasAsync(string identificacion, DateTime fechaInicial, DateTime fechaFin);
    }
}
