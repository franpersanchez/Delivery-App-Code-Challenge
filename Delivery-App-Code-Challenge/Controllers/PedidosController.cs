using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> logger;

        public PedidosController(ILogger<PedidosController> logger)
        {
            this.logger = logger;
        }
    }
}
