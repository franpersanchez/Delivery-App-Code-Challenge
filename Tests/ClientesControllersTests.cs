using DB.Interfaces;
using Delivery_App_Code_Challenge.Controllers;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace YourNamespace.Tests
{
    public class ClientesControllerTests
    {
        //Test data
        private List<Cliente> GetTestClientes()
        {
            return new List<Cliente>
            {
                new Cliente
                {
                    Nombre = "John",
                    Apellidos = "Doe",
                    Telefono = "123-456-7890",
                    Email = "john@example.com",
                    Ubicacion_latitud = "40.7128",
                    Ubicacion_longitud = "-74.0060"
                },
                new Cliente
                {
                    Nombre = "Jane",
                    Apellidos = "Smith",
                    Telefono = "987-654-3210",
                    Email = "jane@example.com",
                    Ubicacion_latitud = "34.0522",
                    Ubicacion_longitud = "-118.2437"

                }
            };
        }

        /// <summary>
        /// Test for Get method /clientes/crea, en el caso de crear 2 nuevos clientes
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllClients_Returns_OkResult_WithListOfClientes()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Cliente>>();
            mockRepository.Setup(repo => repo.GetAllAsync(
            It.IsAny<Expression<Func<Cliente, bool>>>(),
            It.IsAny<Func<IQueryable<Cliente>, IOrderedQueryable<Cliente>>>(),
            It.IsAny<Func<IQueryable<Cliente>, IIncludableQueryable<Cliente, object>>>(),
            true)).
            ReturnsAsync(GetTestClientes());

            var controller = new ClientesController(mockRepository.Object);

            // Act
            var result = await controller.GetAllClients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Cliente>>(okResult.Value);
            Assert.NotEmpty(model);
        }

        /// <summary>
        /// Test for Get method /clientes/muestra-todos in case of no Data in the database
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllClients_Returns_NotFound_WhenNoClientes()
        {
            // Data: Empty list to simulate no clients

            // Arrange
            var mockRepository = new Mock<IRepository<Cliente>>();
            mockRepository.Setup(repo => repo.GetAllAsync(
                It.IsAny<Expression<Func<Cliente, bool>>>(),
                It.IsAny<Func<IQueryable<Cliente>, IOrderedQueryable<Cliente>>>(),
                It.IsAny<Func<IQueryable<Cliente>, IIncludableQueryable<Cliente, object>>>(),
                true))
                .ReturnsAsync(new List<Cliente>());

            var controller = new ClientesController(mockRepository.Object);

            // Act
            var result = await controller.GetAllClients();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No se encontraron clientes", notFoundResult.Value);
        }

        /// <summary>
        /// Test for POST method /clientes/crea in case of no correct modelState sent
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddNewClient_Returns_BadRequest_When_Model_Is_Invalid()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Cliente>>();
            var controller = new ClientesController(mockRepository.Object);
            var newCliente = new Cliente(); // An empty Cliente, which should trigger ModelState errors

            // Simulate ModelState errors
            controller.ModelState.AddModelError("Nombre", "The Nombre field is required.");
            controller.ModelState.AddModelError("Apellidos", "The Apellidos field is required.");

            // Act
            var result = await controller.AddNewClient(newCliente);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<Cliente>>(result);
            Assert.IsType<BadRequestResult>(badRequestResult.Result);
            mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Cliente>()), Times.Never);
        }

        /// <summary>
        /// Test for POST method /clientes/crea in case of correct modelState sent
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddNewClient_Returns_CreatedAtAction_When_Model_Is_Valid()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Cliente>>();
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Cliente>()))
                .Verifiable();

            var controller = new ClientesController(mockRepository.Object);
            var newCliente = new Cliente
            {
                Nombre = "Jane",
                Apellidos = "Smith",
                Telefono = "987-654-3210",
                Email = "jane@example.com",
                Ubicacion_latitud = "34.0522",
                Ubicacion_longitud = "-118.2437"

            };

            // Act
            var result = await controller.AddNewClient(newCliente);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(ClientesController.AddNewClient), createdAtActionResult.ActionName);
            mockRepository.Verify(repo => repo.AddAsync(newCliente), Times.Once);
        }

    }
}
