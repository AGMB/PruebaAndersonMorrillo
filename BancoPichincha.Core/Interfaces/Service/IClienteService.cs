using BancoPichincha.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoPichincha.Core.Interfaces.Service
{
    public  interface IClienteService
    {
        Task<IList<ClienteDto>> ObtenerClientesAsync();
        Task<ClienteDto> ObtenerClienteAsync(int clienteid);
        Task<bool> GuardarClienteAsync(ClienteAddDto cliente);
        Task<bool> ActualizarClienteAsync(ClienteDto cliente);
        Task<bool> EliminarClienteAsync(int clienteid);
        Task<bool> ExisteClienteAsync(int clienteid);
    }
}
