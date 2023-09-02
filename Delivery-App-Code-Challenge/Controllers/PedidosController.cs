using Microsoft.AspNetCore.Mvc;
using DB;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> logger;
        private readonly DeliveryAppContext context;

        public PedidosController(DeliveryAppContext context, ILogger<PedidosController> logger)
        {
            this.logger = logger;
            this.context = context;
        }

        [HttpGet("/check-api")]
        public async Task<IActionResult> CheckAPI()
        {
            return Ok("API correctly running");
        }


    }
}
