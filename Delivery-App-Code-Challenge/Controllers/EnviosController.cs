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
        public EnviosController(IRepository<Pedido> pedidoRepository,
                                IRepository<Envio> envioRepository
                                )
        {
            _pedidoRepository = pedidoRepository;
            _envioRepository = envioRepository;

        }

        [HttpPost("/envio/crear")]
        public async Task<ActionResult<Envio>> AddNewEnvio(Envio newEnvio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _envioRepository.AddAsync(newEnvio);

            return CreatedAtAction(nameof(AddNewEnvio), new { id = newEnvio.Id }, newEnvio);
        }

        [HttpGet("/envio/muestra-todos")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAllEnvios()
        {
            var envios = await _envioRepository.GetAllAsync();
            if (envios.Any())
            {
                return Ok(envios);
            }
            else
            {
                return NotFound("Se se han encontrado Envios");
            }
        }

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
