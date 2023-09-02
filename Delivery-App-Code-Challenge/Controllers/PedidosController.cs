using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class PedidosController : ControllerBase
    {
 
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Envio> _envioRepository;

        public PedidosController(IRepository<Pedido> pedidoRepository,
                                IRepository<Envio> envioRepository
                                )
        {
            _pedidoRepository = pedidoRepository;
            _envioRepository = envioRepository;

        }

        /// <summary>
        /// Crea un nuevo Pedido
        /// </summary>
        /// <param name="newPedido"></param>
        /// <returns></returns>
        [HttpPost("/pedidos/crea")]
        public async Task<ActionResult<Pedido>> AddNewPedido(Pedido newPedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pedidoRepository.AddAsync(newPedido);

            return CreatedAtAction(nameof(AddNewPedido), new { id = newPedido.Id }, newPedido);

        }

        /// <summary>
        /// Crea un rango de Pedidos a la vez.
        /// </summary>
        /// <param name="newPedidos"></param>
        /// <returns></returns>
        [HttpPost("/pedidos/crea-rango")]
        public async Task<ActionResult<IEnumerable<Pedido>>> AddNewPedidoRange(List<Pedido> newPedidos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pedidoRepository.AddRangeAsync(newPedidos);

            return CreatedAtAction(nameof(AddNewPedidoRange), new { }, newPedidos);

        }

        /// <summary>
        /// Muestra todos los Pedidos existentes en la base de datos.
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        [HttpGet("/pedidos/muestra-todos")]
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
            return pedidos.Any() ? pedidos : NotFound("No se encontraron pedidos");
        }

        /// <summary>
        /// Actualiza el estado de un Pedido -> pendiente(default), aceptado, pagado, enviado, entregado.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nuevoEstado"></param>
        /// <returns></returns>
        [HttpPut("/pedidos/{id}/actualiza-estado")]
        public async Task<IActionResult> UpdatePedido(long id, [FromQuery] EstadoPedido nuevoEstado)
        {
            var pedido = await _pedidoRepository.GetSingleOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            //si se pretende actualiar como ENTREGADO, entonces se saca el Pedido del conjunto albergado en el Envio asociado:
            if (nuevoEstado == EstadoPedido.Entregado)
            {
                var envio = await _envioRepository.GetSingleOrDefaultAsync(e => e.Pedidos.Any(p => p.Id == id));
                if (envio != null)
                {
                    envio.Pedidos.Remove(pedido);
                }
            }
            //actualizamos el estado del Pedido
            pedido.EstadoPedido = nuevoEstado;
            _pedidoRepository.Update(pedido);
            return Ok("Pedido con ID: " + id + ", actualizado como: " + nuevoEstado);
        }
    }
}
