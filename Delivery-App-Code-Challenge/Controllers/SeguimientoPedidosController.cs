using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class SeguimientoPedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly IRepository<RegistroUbicacion> _registroUbicacionRepository;
        private readonly IRepository<Envio> _envioRepository;

        public SeguimientoPedidosController(IRepository<Pedido> pedidoRepository,
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

        [HttpGet("/where-is-my-order/{order_id}")]
        public async Task<ActionResult<IEnumerable<RegistroUbicacion>>> WhereIsMyOrder(long order_id)
        {
            try
            {
                // Buscar el pedido por su ID
                var pedido = await _pedidoRepository.GetSingleOrDefaultAsync(p => p.Id == order_id);

                if (pedido == null)
                {
                    return NotFound("Pedido not found");
                }

                // Encontrar uno de los envíos asociados a este pedido, si existe
                var envio = await _envioRepository.GetSingleOrDefaultAsync(x => x.Pedidos.Any(x => x.Id == order_id));

                if (envio == null)
                {
                    return NotFound("No info for this Envio");
                }

                // Obtener el vehículo asociado al envío
                var vehiculo = envio.Vehiculo;

                if (vehiculo == null)
                {
                    return NotFound("No info for this Vehiculo");
                }

                // Obtener la lista de registros de ubicación del vehículo ordenada por fecha de registro
                var registrosUbicacion = vehiculo.RegistroUbicaciones
                    .OrderByDescending(r => r.FechaRegistro)
                    .ToList();

                return Ok(registrosUbicacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
