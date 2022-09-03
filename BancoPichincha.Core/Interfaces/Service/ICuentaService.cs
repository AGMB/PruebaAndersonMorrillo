using BancoPichincha.Core.Models.Dto;
using BancoPichincha.Core.Models.Entities;

namespace BancoPichincha.Core.Interfaces.Service
{
    public interface ICuentaService
    {
        Task<IList<CuentaDto>> ObtenerCuentasAsync();
        Task<CuentaDto> ObtenerCuentaPorNumeroCuentaAsync(string numeroCuenta);
        Task<IList<CuentaClienteDto>> ObtenerCuentasPorClienteAsync(int clienteId);       
        Task<bool> GuardarCuentaAsync(CuentaDto cuenta);
        Task<bool> ActualizarCuentaAsync(CuentaDto cuenta);
        Task<bool> EliminarCuentaAsync(string numeroCuenta);
    }
}
