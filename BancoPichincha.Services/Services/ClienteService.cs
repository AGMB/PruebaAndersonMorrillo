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
    public class ClienteService : IClienteService
    {
        #region Propiedades

        private readonly IClienteRepository _clienteRepository;
        #endregion

        #region Constructor
        public ClienteService (IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarClienteAsync(ClienteDto cliente)
        {
            var clienteActualizar = await _clienteRepository.GetByIdAsync(cliente.ClienteID);
            if (clienteActualizar is null)
            {
                return false;
            };

            clienteActualizar.ClienteId = cliente.ClienteID;
            clienteActualizar.ClienteIdentificacion = cliente.Identificacion;
            clienteActualizar.Nombre = cliente.Nombres;
            clienteActualizar.Genero = cliente.Genero;
            clienteActualizar.Edad = cliente.Edad;
            clienteActualizar.Direccion = cliente.Direccion;
            clienteActualizar.Telefono = cliente.Telefono;
            clienteActualizar.Contrasena = cliente.Contrasena;
            clienteActualizar.Estado = cliente.Estado;

            _clienteRepository.Update(clienteActualizar);
            await _clienteRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarClienteAsync(int clienteid)
        {
            var clienteEliminar = await _clienteRepository.GetByIdAsync(clienteid);
            if (clienteEliminar is null)
                return false;

            _clienteRepository.Remove(clienteEliminar);
            await _clienteRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExisteClienteAsync(int clienteid)
        {
            bool existeCliente = false;
            var cliente = await _clienteRepository.GetByIdAsync(clienteid);
            if (cliente is not null)
            {
                existeCliente = true;
            }
            return existeCliente;
        }

        public async Task<bool> GuardarClienteAsync(ClienteAddDto clienteDto)
        {
            var clienteGuardar = new Cliente
            {
                ClienteIdentificacion = clienteDto.Identificacion,
                Nombre = clienteDto.Nombres,
                Genero = clienteDto.Genero,
                Edad = clienteDto.Edad,
                Direccion = clienteDto.Direccion,
                Telefono = clienteDto.Telefono,
                Contrasena = clienteDto.Contrasena,
                Estado = clienteDto.Estado,
            };

            await _clienteRepository.AddAsync(clienteGuardar);

            await _clienteRepository.SaveChangesAsync();

            return true;
        }

        public async Task<ClienteDto> ObtenerClienteAsync(int clienteid)
        {
            var cliente = await _clienteRepository.GetByIdAsync(clienteid);
            if (cliente == null)
                return null;

            var result = new ClienteDto
            {
                ClienteID = cliente.ClienteId,
                Identificacion = cliente.ClienteIdentificacion,
                Nombres = cliente.Nombre,
                Genero = cliente.Genero,
                Edad = cliente.Edad,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono,
                Contrasena = cliente.Contrasena,
                Estado = cliente.Estado
            };
            return result;
        }

        public async Task<IList<ClienteDto>> ObtenerClientesAsync()
        {
            var result = await _clienteRepository.GetAllIQuerable()
                          .Select(x => new ClienteDto
                          {
                              ClienteID = x.ClienteId,
                              Identificacion = x.ClienteIdentificacion,
                              Nombres = x.Nombre,
                              Genero = x.Genero,
                              Edad = x.Edad,
                              Direccion = x.Direccion,
                              Telefono = x.Telefono,
                              Contrasena = x.Contrasena,
                              Estado = x.Estado
                          }).ToListAsync();

            return result;
        } 
        #endregion
    }
}
