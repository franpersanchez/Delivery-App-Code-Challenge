using DB;
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

        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IRepository<Envio> _envioRepository;
        private readonly DeliveryAppContext _appRepo;

        public EnviosController(IRepository<Pedido> pedidoRepository,
                                IRepository<Envio> envioRepository,
                                DeliveryAppContext appRepo
                                )
        {
            _pedidoRepository = pedidoRepository;
            _envioRepository = envioRepository;
            _appRepo = appRepo;

        }

        /// <summary>
        /// Crea un nuevo envio. Previamente debe existir el Pedido.
        /// </summary>
        /// <param name="nuevoEnvio"></param>
        /// <returns></returns>
        [HttpPost("/envio/crea")]
        public async Task<ActionResult<Envio>> AddNewEnvio(Envio nuevoEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _envioRepository.AddAsync(nuevoEnvio);

            return CreatedAtAction(nameof(AddNewEnvio), new { id = nuevoEnvio.Id }, nuevoEnvio);
        }

        /// <summary>
        /// Muestra todos los Envios existentes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/envio/muestra-todos")]
        public async Task<ActionResult<IEnumerable<Envio>>> GetAllEnvios()
        {
            var envios = await _appRepo.Envios.Include(e=>e.Pedidos).ToListAsync();
            if (envios.Any())
            {
                return Ok(envios);
            }
            else
            {
                return NotFound("No se han encontrado Envios");
            }
        }

        /// <summary>
        /// Asigna un Pedido a un Envio para ser transportado.El Pedido debe existir y no estar ya enviado o entregado o previamente añadido al Envio.
        /// </summary>
        /// <param name="pedido_id"></param>
        /// <param name="envio_id"></param>
        /// <returns></returns>
        [HttpPut("/pedido/{pedido_id}/a-envio/{envio_id}")]
        public async Task<ActionResult<Envio>> AssignPedidoToEnvio(long pedido_id, long envio_id)
        {
            var pedido = await _pedidoRepository.GetSingleOrDefaultAsync(p => p.Id == pedido_id && p.EstadoPedido != EstadoPedido.Enviado && p.EstadoPedido != EstadoPedido.Entregado);
            var envio = await _envioRepository.GetSingleOrDefaultAsync(_ => _.Id == envio_id);

            if (pedido == null || envio == null)
            {
                return NotFound("Sin pedidos o envios encontrados");
            }
            else
            {
                //el conjunto de Pedidos es un HashSet, por tanto si ya existe ese Pedido en este Envio, al intentar añadirlo retornoará false:
                if (envio.Pedidos.Add(pedido))
                {
                    //marcamos el estado como "ENVIADO" en el momento de agregar el pedido al envio
                    pedido.EstadoPedido = EstadoPedido.Enviado;
                    _pedidoRepository.Update(pedido);

                    _envioRepository.Update(envio);
                    return Ok(envio);
                }
                else
                {
                    //No debería ocurrir pero si de alguna forma se salta el check de estado.
                    return Conflict("Conflicto al agregar el pedido al envío. El pedido ya se encuentra en el envio.");
                }

            }
        }

    }
}
