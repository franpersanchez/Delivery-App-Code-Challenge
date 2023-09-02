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
            if(newEstado == EstadoPedido.Entregado)
            {
                var envio = await _envioRepository.GetSingleOrDefaultAsync(e=>e.Pedidos.Any(p=>p.Id==id));
                if(envio != null)
                {
                    envio.Pedidos.Remove(pedido);
                }
            }
            pedido.EstadoPedido = newEstado;
            _pedidoRepository.Update(pedido);
            return Ok("pedido con ID: " + id + ", actualizado como: " + newEstado);
        }

        [HttpPost("/vehicle/add")]
        public async Task<ActionResult<Vehiculo>> AddNewVehiculo(Vehiculo newVehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _vehiculoRepository.AddAsync(newVehiculo);

            return CreatedAtAction(nameof(AddNewVehiculo), new { id = newVehiculo.Id }, newVehiculo);
        }

        [HttpGet("/vehicle/get-all")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetAllVehiculos()
        {
            var vehiculos = await _vehiculoRepository.GetAllAsync();
            if (vehiculos.Any())
            {
                return Ok(vehiculos);
            }
            else
            {
                return NotFound("No vehiculos found");
            }
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

        [HttpPut("localization/vehicle-update-position")]
        public async Task<IActionResult> UpdateUbicacion([FromBody] RegistroUbicacion nuevaUbicacion)
        {
            try
            {
                var vehiculo = await _vehiculoRepository.GetSingleOrDefaultAsync(v => v.Id == nuevaUbicacion.VehiculoId);

                if (vehiculo == null)
                {
                    return NotFound("Vehículo not found");
                }

                // Actualizar la posición del vehículo
                var newRegistroUbicacion = await _registroUbicacionRepository.AddAsync(nuevaUbicacion);

                return Ok("Ubicación del vehículo actualizada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpGet("localization-history/vehicle/{vehicle_id}")]
        public async Task<ActionResult<IEnumerable<RegistroUbicacion>>> GetRegistroUbicacionForVehicle(long vehicle_id)
        {
            var vehicle = await _vehiculoRepository.GetSingleOrDefaultAsync(v=>v.Id== vehicle_id);
            if(vehicle == null)
            {
                return NotFound("Vehiculo not found");
            }
            else
            {
                var registroUbicacion = vehicle.RegistroUbicaciones;
                if (registroUbicacion.Any())
                {
                    return registroUbicacion.ToList();
                }
                else
                {
                    return NotFound("No RegistroUbicaciones for this Vehiculo yet");
                }
            }
        }

        [HttpGet("current-localization/vehicle/{vehicle_id}")]
        public async Task<ActionResult<IEnumerable<RegistroUbicacion>>> GetCurrentRegistroUbicacionForVehicle(long vehicle_id)
        {
            var vehicle = await _vehiculoRepository.GetSingleOrDefaultAsync(v => v.Id == vehicle_id);
            if (vehicle == null)
            {
                return NotFound("Vehiculo not found");
            }
            else
            {
                var registroUbicacion = vehicle.RegistroUbicaciones;
                if (registroUbicacion.Any())
                {
                    var registroMasReciente = registroUbicacion.OrderByDescending(r => r.FechaRegistro).First();
                    return Ok(registroMasReciente);
                }
                else
                {
                    return NotFound("No RegistroUbicaciones for this Vehiculo yet");
                }
            }
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
                var envio = await _envioRepository.GetSingleOrDefaultAsync(x=>x.Pedidos.Any(x=>x.Id==order_id));

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
