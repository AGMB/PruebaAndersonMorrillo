using BancoPichincha.Core.AppSettings;
using BancoPichincha.Core.Interfaces.Repository;
using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Core.Models.Dto;
using BancoPichincha.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Services.Services
{
    public class MovimientoService : IMovimientoService
    {
        #region Propiedades
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly AppSettings _appSettings;
        #endregion

        #region Constructor
        public MovimientoService(IMovimientoRepository movimientoRepository, ICuentaRepository cuentaRepository, IClienteRepository clienteRepository, IOptions<AppSettings> options)
        {
            _movimientoRepository = movimientoRepository;
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
            _appSettings = options.Value;
        }
        #endregion

        #region Metodos Publicos
        public async Task<MovimientoDto> ObtenerMovimientoPorIdAsync(int movimientoId)
        {
            var movimiento = await _movimientoRepository.GetByIdAsync(movimientoId);
            if (movimiento == null)
                return null;

            var movimientoDto = new MovimientoDto
            {
                MovimientoId = movimiento.MovimientoId,
                NumeroCuenta = movimiento.NumeroCuenta,
                Fecha = movimiento.Fecha,
                TipoMovimiento = movimiento.TipoMovimiento,
                SaldoInicial = movimiento.SaldoInicial,
                Valor = movimiento.Valor,
                SaldoFinal = movimiento.SaldoFinal,
            };

            return movimientoDto;
        }

        public async Task<IList<MovimientoDto>> ObtenerMovimientosAsync()
        {
            var result = await _movimientoRepository.GetAllIQuerable()
                         .Select(x => new MovimientoDto
                         {
                             MovimientoId = x.MovimientoId,
                             NumeroCuenta = x.NumeroCuenta,
                             Fecha = x.Fecha,
                             TipoMovimiento = x.TipoMovimiento,
                             SaldoInicial = x.SaldoInicial,
                             Valor = x.Valor,
                             SaldoFinal = x.SaldoFinal,
                         }).OrderBy(x => x.NumeroCuenta).ThenBy(x => x.Fecha).ToListAsync();

            return result;
        }

        public async Task<IList<MovimientoDto>> ObtenerMovimientosPorCuentaAsync(string numeroCuenta)
        {
            var result = await _movimientoRepository.GetAllIQuerable()
                          .Where(x => x.NumeroCuenta.Equals(numeroCuenta))
                          .Select(x => new MovimientoDto
                          {
                              MovimientoId = x.MovimientoId,
                              NumeroCuenta = x.NumeroCuenta,
                              Fecha = x.Fecha,
                              TipoMovimiento = x.TipoMovimiento,
                              SaldoInicial = x.SaldoInicial,
                              Valor = x.Valor,
                              SaldoFinal = x.SaldoFinal,
                          }).OrderByDescending(x => x.Fecha).ToListAsync();

            return result;
        }

        public async Task<ResponseModel> RegistrarMovimientoSaldosAsync(MovimientoRegistroDto movimiento)
        {
            var response = new ResponseModel();
            if (EsOperacionCredito(movimiento.Monto))
            {
                var montoFinal = movimiento.SaldoInicial + movimiento.Monto;
                var registroMovimiento = new Movimiento
                {
                    NumeroCuenta = movimiento.NumeroCuenta,
                    Fecha = DateTime.Now,
                    TipoMovimiento = "Deposito",
                    SaldoInicial = movimiento.SaldoInicial,
                    Valor = movimiento.Monto,
                    SaldoFinal = montoFinal
                };
                await _movimientoRepository.AddAsync(registroMovimiento);
                await _movimientoRepository.SaveChangesAsync();
                response.Mensaje = $"Transaccion # {registroMovimiento.MovimientoId} Realizada con éxito";
                response.StatusCode = 201;
            }
            else
            {
                var montoDisponible = movimiento.SaldoInicial;
                if (montoDisponible < Math.Abs(movimiento.Monto))
                {
                    response.IsSuccess = false;
                    response.Mensaje = "Saldo no Disponible";
                }
                else if (await ExcedeCupoDiarioPorCuenta(movimiento.NumeroCuenta, Math.Abs(movimiento.Monto)))
                {
                    response.IsSuccess = false;
                    response.Mensaje = "Cupo Diario Excedido";
                }
                else
                {
                    var montoFinal = montoDisponible - Math.Abs(movimiento.Monto);
                    var registroMovimiento = new Movimiento
                    {
                        NumeroCuenta = movimiento.NumeroCuenta,
                        Fecha = DateTime.Now,
                        TipoMovimiento = "Retiro",
                        SaldoInicial = movimiento.SaldoInicial,
                        Valor = movimiento.Monto,
                        SaldoFinal = montoFinal
                    };
                    await _movimientoRepository.AddAsync(registroMovimiento);
                    await _movimientoRepository.SaveChangesAsync();
                    response.Mensaje = $"Transaccion # {registroMovimiento.MovimientoId} Realizada con éxito";
                    response.StatusCode = 201;
                }
            }

            return response;
        }

        public async Task<bool> ActualizarMovimientoAsync(MovimientoDto movimiento)
        {
            var movimientoActualizar = await _movimientoRepository.GetByIdAsync(movimiento.MovimientoId);
            if (movimientoActualizar is null)
                return false;

            movimientoActualizar.NumeroCuenta = movimiento.NumeroCuenta;
            movimientoActualizar.TipoMovimiento = movimiento.TipoMovimiento;
            movimientoActualizar.Fecha = movimiento.Fecha;
            movimientoActualizar.SaldoInicial = movimiento.SaldoInicial;
            movimientoActualizar.Valor = movimiento.Valor;
            movimientoActualizar.SaldoFinal = movimiento.SaldoFinal;

            _movimientoRepository.Update(movimientoActualizar);

            await _movimientoRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarMovimientoAsync(int movimientoid)
        {
            var movimientoEliminar = await _movimientoRepository.GetByIdAsync(movimientoid);
            if (movimientoEliminar is null)
                return false;

            _movimientoRepository.Remove(movimientoEliminar);
            await _movimientoRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IList<ReporteDto>> GenerarReportePorClienteYRangoFechasAsync(string identificacion, DateTime fechaInicial, DateTime fechaFin)
        {
            if(fechaInicial.Equals(fechaFin))
                fechaFin =  DateTime.ParseExact(fechaInicial.ToString("dd-MM-yyyy") + " 23:59:59", "dd-MM-yyyy HH:mm:ss", null);
            var result = await (from cl in _clienteRepository.GetAllIQuerable()
                                join cu in _cuentaRepository.GetAllIQuerable()
                                on cl.ClienteId equals cu.ClienteId
                                join mo in _movimientoRepository.GetAllIQuerable()
                                on cu.NumeroCuenta equals mo.NumeroCuenta
                                where cl.ClienteIdentificacion == identificacion && (mo.Fecha >= fechaInicial && mo.Fecha <= fechaFin)
                                orderby mo.Fecha descending
                                select new ReporteDto
                                {
                                    Fecha = mo.Fecha.ToString("MM-dd-yyyy"),
                                    Cliente = cl.Nombre,
                                    NumeroCuenta = cu.NumeroCuenta,
                                    Tipo = cu.Tipo,
                                    SaldoInicial = mo.SaldoInicial,
                                    Estado = cu.Estado,
                                    Movimiento = mo.Valor,
                                    SaldoDisponible = mo.SaldoFinal

                                }).ToListAsync();

            return result;
        } 
        #endregion

        #region Metodos Privados
        private bool EsOperacionCredito(decimal montoTransaccion)
        {
            return montoTransaccion > 0;
        }

        private async Task<decimal> ObtenerValorUltimoMovimientoCuenta(string numeroCuenta)
        {
            var result = await _movimientoRepository.GetAllIQuerable()
                        .Where(x => x.NumeroCuenta == numeroCuenta)
                        .OrderByDescending(x => x.Fecha)
                        .Select(x => x.SaldoFinal).FirstOrDefaultAsync();

            return result;
        }

        private async Task<bool> ExcedeCupoDiarioPorCuenta(string numeroCuenta, decimal monto)
        {
            DateTime fechaActual = DateTime.Now.Date;
            var cupoRetiroUtilizado = await _movimientoRepository.GetAllIQuerable()
                                       .Where(x => x.NumeroCuenta.Equals(numeroCuenta) && x.Fecha.Date == fechaActual && x.TipoMovimiento.Equals("Retiro"))
                                       .SumAsync(x => Math.Abs(x.Valor));

            return cupoRetiroUtilizado + monto > _appSettings.configuracion.LimiteDiario;
        } 
        #endregion


    }
}
