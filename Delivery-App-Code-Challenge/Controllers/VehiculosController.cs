using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{

    [ApiController]
    [Route("[controller")]
    public class VehiculosController : ControllerBase
    {
        private readonly IRepository<Vehiculo> _vehiculoRepository;

        public VehiculosController(IRepository<Vehiculo> vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }


        [HttpPost("/vehiculo/crear")]
        public async Task<ActionResult<Vehiculo>> AddNewVehiculo(Vehiculo newVehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _vehiculoRepository.AddAsync(newVehiculo);

            return CreatedAtAction(nameof(AddNewVehiculo), new { id = newVehiculo.Id }, newVehiculo);
        }

        [HttpGet("/vehiculo/muestra-todos")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetAllVehiculos()
        {
            var vehiculos = await _vehiculoRepository.GetAllAsync();
            if (vehiculos.Any())
            {
                return Ok(vehiculos);
            }
            else
            {
                return NotFound("Sin vehiculos en la base de datos");
            }
        }

    }
}
