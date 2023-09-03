using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class ClientesController : ControllerBase
    {
        private readonly IRepository<Cliente> _clienteRepository;


        public ClientesController(IRepository<Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="nuevoCliente"></param>
        /// <returns></returns>
        [HttpPost("/clientes/crea")]
        public async Task<ActionResult<Cliente>> AddNewClient(Cliente nuevoCliente)
        {
            if (!ModelState.IsValid || nuevoCliente==null)
            {
                return BadRequest();
            }
            await _clienteRepository.AddAsync(nuevoCliente);
            return CreatedAtAction(nameof(AddNewClient), new { id = nuevoCliente.Id }, nuevoCliente);
        }

        /// <summary>
        /// Muestra todos los clientes existentes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/clientes/muestra-todos")]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetAllClients()
        {
            var result = await _clienteRepository.GetAllAsync();
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound("No se encontraron clientes");
            }
        }


    }
}
