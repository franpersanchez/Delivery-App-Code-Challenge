using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class EnviosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly IRepository<RegistroUbicacion> _registroUbicacionRepository;
        private readonly IRepository<Envio> _envioRepository;

        public EnviosController(IRepository<Pedido> pedidoRepository,
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

        [HttpPost("/shipment/add")]
        public async Task<ActionResult<Envio>> AddNewEnvio(Envio newEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _envioRepository.AddAsync(newEnvio);

            return CreatedAtAction(nameof(AddNewEnvio), new { id = newEnvio.Id }, newEnvio);
        }

        [HttpGet("/shipment/get-all")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAllEnvios()
        {
            var envios = await _envioRepository.GetAllAsync();
            if (envios.Any())
            {
                return Ok(envios);
            }
            else
            {
                return NotFound("No Envios found");
            }
        }

        [HttpPut("/order/{order_id}/to-shipment/{shipment_id}")]
        public async Task<ActionResult<Envio>> AssignPedidoToEnvio(long order_id, long shipment_id)
        {
            var pedido = await _pedidoRepository.GetSingleOrDefaultAsync(p => p.Id == order_id);
            var envio = await _envioRepository.GetSingleOrDefaultAsync(_ => _.Id == shipment_id);

            if (pedido == null || envio == null)
            {
                return NotFound("No pedidos or Envios found");
            }
            else
            {
                envio.Pedidos.Add(pedido);
                _envioRepository.Update(envio);
                return Ok(envio);
            }
        }


    }
}
