using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Core.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BancoPichincha.API.Test
{
    [TestClass]
    public class ClienteControllerTest
    {
        [TestMethod]
        [Description("Retorna una lista de todos los clientes")]
        public async Task ListarCliente_ExistenClientes_RetornaListaClientes()
        {
            //Arrange
            IClienteService _clienteService = Substitute.For<IClienteService>();
            _clienteService.ObtenerClientesAsync().Returns(new List<ClienteDto>
            {
                new ClienteDto
                {
                    Identificacion = "123456",
                    Nombres = "Anderso Morrillo",
                    Genero = "Masculino",
                    Edad = 28,
                    Direccion = "Quito",
                    Telefono = "0981160765",
                    Estado = true,
                    Contrasena = "12345sa"
                },
                new ClienteDto
                {
                    Identificacion = "67894",
                    Nombres = "Gustavo Bravo",
                    Genero = "Masculino",
                    Edad = 15,
                    Direccion = "Chone",
                    Telefono = "0994565465",
                    Estado = true,
                    Contrasena = "4546UIO"
                }
            });

            var totalClientesEsperados = 2;
            var codigoDeRespuestaEsperado = StatusCodes.Status200OK;

            var clienteController = new ClienteController(_clienteService);

            //Act
            var response = (OkObjectResult) await clienteController.ListarClientes();
            var listaCliente = (List<ClienteDto>)response.Value;

            //Assert
            Assert.AreEqual(codigoDeRespuestaEsperado, response.StatusCode);
            Assert.AreEqual(totalClientesEsperados, listaCliente.Count);
        }

        [TestMethod]
        [Description("Si existe un cliente en base a la identificacion proporcionada, retorna dicho cliente")]
        public async Task ObtenerCliente_ExisteCliente_RetornaCliente()
        {
            //Arrange
            IClienteService _clienteService = Substitute.For<IClienteService>();
            _clienteService.ObtenerClienteAsync(Arg.Any<int>()).Returns(
                new ClienteDto
                {
                    ClienteID = 1,
                    Identificacion = "123456",
                    Nombres = "Anderso Morrillo",
                    Genero = "Masculino",
                    Edad = 28,
                    Direccion = "Quito",
                    Telefono = "0981160765",
                    Estado = true,
                    Contrasena = "12345sa"
                });

            var identificacionEsperada = "123456";
            var codigoDeRespuestaEsperado = StatusCodes.Status200OK;

            var clienteID = 1;

            var clienteController = new ClienteController(_clienteService);

            //Act
            var response = (OkObjectResult)await clienteController.ObtenerCliente(clienteID);
            var cliente = (ClienteDto) response.Value;

            //Assert
            Assert.AreEqual(codigoDeRespuestaEsperado, response.StatusCode);
            Assert.AreEqual(identificacionEsperada, cliente.Identificacion);
            Assert.IsNotNull(cliente);
        }

        [TestMethod]
        [Description("Si no existe un cliente en base a la identificacion proporcionada, retorna Status 404")]
        public async Task ObtenerCliente_NoExisteCliente_RetornaNotFound()
        {
            //Arrange
            IClienteService _clienteService = Substitute.For<IClienteService>();
            _clienteService.ObtenerClienteAsync(Arg.Any<int>()).ReturnsNull();

            var codigoDeRespuestaEsperado = StatusCodes.Status404NotFound;

            var clienteID = 1;

            var clienteController = new ClienteController(_clienteService);

            //Act
            var response = (NotFoundObjectResult)await clienteController.ObtenerCliente(clienteID);


            //Assert
            Assert.AreEqual(codigoDeRespuestaEsperado, response.StatusCode);
        }
    }
}