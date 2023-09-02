using Microsoft.AspNetCore.Mvc;
using DB;
using DB.Interfaces;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.EntityFrameworkCore.Migrations;

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
                                ILogger<PedidosController> logger)
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

        [HttpPost("/add-new-order")]
        public async Task<ActionResult<Pedido>> AddNewPedido(Pedido newPedido)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pedidoRepository.AddAsync(newPedido);
            return CreatedAtAction(nameof(AddNewPedido), new { id =  newPedido.Id }, newPedido);
        }


    }
}
