using BancoPichincha.Core.Interfaces.Repository;
using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Core.Models.Dto;
using BancoPichincha.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Services.Services
{
    public class CuentaService : ICuentaService
    {
        #region Propiedades
        private readonly ICuentaRepository _cuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        #endregion

        #region Constructor
        public CuentaService(ICuentaRepository cuentaRepository, IClienteRepository clienteRepository)
        {
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarCuentaAsync(CuentaDto cuenta)
        {
            var cuentaActualizar = await _cuentaRepository.GetByIdAsync(cuenta.NumeroCuenta);
            if (cuentaActualizar is null)
            {
                return false;
            };

            cuentaActualizar.SaldoInicial = cuenta.SaldoInicial;
            cuentaActualizar.Estado = cuenta.Estado;
            cuentaActualizar.Tipo = cuenta.Tipo;
            _cuentaRepository.Update(cuentaActualizar);
            await _cuentaRepository.SaveChangesAsync();

            return true;
        }

        public Task<bool> ActualizarCuentaWithAttach(CuentaDto cuenta)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EliminarCuentaAsync(string numeroCuenta)
        {
            var cuentaEliminar = await _cuentaRepository.GetByIdAsync(numeroCuenta);
            if (cuentaEliminar is null)
                return false;

            _cuentaRepository.Remove(cuentaEliminar);
            await _cuentaRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> GuardarCuentaAsync(CuentaDto cuenta)
        {
            var cuentaGuardar = new Cuenta
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                Tipo = cuenta.Tipo,
                SaldoInicial = cuenta.SaldoInicial,
                Estado = cuenta.Estado,
                ClienteId = cuenta.ClienteId,
            };

            await _cuentaRepository.AddAsync(cuentaGuardar);

            await _cuentaRepository.SaveChangesAsync();

            return true;
        }

        public async Task<CuentaDto> ObtenerCuentaPorNumeroCuentaAsync(string numeroCuenta)
        {
            var result = await _cuentaRepository.GetAllIQuerable()
                         .Where(x => x.NumeroCuenta.Equals(numeroCuenta))
                         .Select(x => new CuentaDto
                         {
                             NumeroCuenta = x.NumeroCuenta,
                             Tipo = x.Tipo,
                             SaldoInicial = x.SaldoInicial,
                             Estado = x.Estado,
                             ClienteId = x.ClienteId
                         }).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IList<CuentaDto>> ObtenerCuentasAsync()
        {
            var result = await _cuentaRepository.GetAllIQuerable()
                         .Select(x => new CuentaDto
                         {
                             NumeroCuenta = x.NumeroCuenta,
                             Tipo = x.Tipo,
                             SaldoInicial = x.SaldoInicial,
                             Estado = x.Estado,
                             ClienteId = x.ClienteId
                         }).ToListAsync();

            return result;
        }

        public async Task<IList<CuentaClienteDto>> ObtenerCuentasPorClienteAsync(int clienteId)
        {

            var result = await (from cl in _clienteRepository.GetAllIQuerable()
                                join cu in _cuentaRepository.GetAllIQuerable()
                                on cl.ClienteId equals cu.ClienteId
                                where cu.ClienteId == clienteId
                                select new CuentaClienteDto
                                {
                                    Identificacion = cl.ClienteIdentificacion,
                                    Nombres = cl.Nombre,
                                    NumeroCuenta = cu.NumeroCuenta,
                                    Tipo = cu.Tipo,
                                    SaldoInicial = cu.SaldoInicial
                                }).ToListAsync();

            return result;

        } 
        #endregion
    }
}
