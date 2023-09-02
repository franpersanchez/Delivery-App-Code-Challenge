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
        private readonly ILogger<PedidosController> _logger;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly IRepository<RegistroUbicacion> _registroUbicacionRepository;
        private readonly IRepository<Envio> _envioRepository;

        public ClientesController(IRepository<Pedido> pedidoRepository,
                                IRepository<Cliente> clienteRepository,
                                IRepository<Vehiculo> vehiculoRepository,
                                IRepository<RegistroUbicacion> registroUbicacionRepository,
                                ILogger<PedidosController> logger,
                                IRepository<Envio> envioRepository
                                )
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _vehiculoRepository = vehiculoRepository;
            _registroUbicacionRepository = registroUbicacionRepository;
            _envioRepository = envioRepository;
            _logger = logger;
        }

        [HttpPost("/client/add")]
        public async Task<ActionResult<Cliente>> AddNewClient(Cliente newCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _clienteRepository.AddAsync(newCliente);
            return CreatedAtAction(nameof(AddNewClient), new { id = newCliente.Id }, newCliente);
        }

        [HttpGet("/client/get-all")]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetAllClients()
        {
            var result = await _clienteRepository.GetAllAsync();
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(204, "No clients found");
            }
        }


    }
}
