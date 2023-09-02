using Microsoft.AspNetCore.Mvc;
using DB;
using DB.Interfaces;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data.Entity;

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
        private readonly IRepository<HistorialUbicacion> _historialUbicacionRepository;

        public PedidosController(IRepository<Pedido> pedidoRepository,
                                IRepository<Cliente> clienteRepository,
                                IRepository<Vehiculo> vehiculoRepository,
                                IRepository<HistorialUbicacion> historialUbicacionRepository,
                                ILogger<PedidosController> logger
                                )
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _vehiculoRepository = vehiculoRepository;
            _historialUbicacionRepository = historialUbicacionRepository;
            _logger = logger;
        }

        [HttpGet("/check-api")]
        public async Task<IActionResult> CheckAPI()
        {
            return Ok("API correctly running");
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


        [HttpPost("/orders/add")]
        public async Task<ActionResult<Pedido>> AddNewPedido(Pedido newPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pedidoRepository.AddAsync(newPedido);

            return CreatedAtAction(nameof(AddNewPedido), new { }, newPedido);

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

        [HttpPut("/orders/{id}/update")]
        public async Task<IActionResult> UpdatePedido(int id, [FromQuery] string estado)
        {
            var pedido = await _pedidoRepository.GetSingleOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            switch (estado)
            {
                case "entregado":
                    pedido.Entregado = true;
                    break;
                case "aceptado":
                    pedido.Aceptado = true;
                    break;
                case "pagado":
                    pedido.Pagado = true;
                    break;
                default:
                    return BadRequest("El valor de 'estado' no es válido.");
            }
            _pedidoRepository.Update(pedido);
            return Ok("pedido actualizado como" + estado);
        }
    }
}
