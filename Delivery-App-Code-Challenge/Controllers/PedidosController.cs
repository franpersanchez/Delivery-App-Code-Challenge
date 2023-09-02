using Microsoft.AspNetCore.Mvc;
using DB;
using DB.Interfaces;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data.Entity;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly IRepository<RegistroUbicacion> _registroUbicacionRepository;
        private readonly IRepository<Envio> _envioRepository;

        public PedidosController(IRepository<Pedido> pedidoRepository,
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

        [HttpPost("/orders/add")]
        public async Task<ActionResult<Pedido>> AddNewPedido(Pedido newPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pedidoRepository.AddAsync(newPedido);

            return CreatedAtAction(nameof(AddNewPedido), new { id = newPedido.Id }, newPedido);

        }


        [HttpPost("/orders/add-range")]
        public async Task<ActionResult<IEnumerable<Pedido>>> AddNewPedidoRange(List<Pedido> newPedidos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pedidoRepository.AddRangeAsync(newPedidos);

            return CreatedAtAction(nameof(AddNewPedidoRange), new { }, newPedidos);

        }

        [HttpGet("/orders/get-all")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAllPedidos([FromQuery] EstadoPedido? estado)
        {
            var pedidos = new List<Pedido>();
            if (estado == null)
            {
                pedidos = await _pedidoRepository.GetAllAsync();
            }
            else
            {
                pedidos = await _pedidoRepository.GetAllAsync(p => p.EstadoPedido == estado);
            }
            return pedidos.Any() ? pedidos : NotFound("No pedidos found");
        }

        [HttpPut("/orders/{id}/update")]
        public async Task<IActionResult> UpdatePedido(long id, [FromQuery] EstadoPedido newEstado)
        {
            var pedido = await _pedidoRepository.GetSingleOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            if (newEstado == EstadoPedido.Entregado)
            {
                var envio = await _envioRepository.GetSingleOrDefaultAsync(e => e.Pedidos.Any(p => p.Id == id));
                if (envio != null)
                {
                    envio.Pedidos.Remove(pedido);
                }
            }
            pedido.EstadoPedido = newEstado;
            _pedidoRepository.Update(pedido);
            return Ok("pedido con ID: " + id + ", actualizado como: " + newEstado);
        }
    }
}
