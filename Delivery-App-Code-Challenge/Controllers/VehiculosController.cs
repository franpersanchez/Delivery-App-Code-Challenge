using DB;
using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Delivery_App_Code_Challenge.Controllers
{

    [ApiController]
    [Route("[controller")]
    public class VehiculosController : ControllerBase
    {
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly IRepository<RegistroUbicacion> _registroUbicacionesRepository;
        private readonly DeliveryAppContext _deliveryAppContext;

        public VehiculosController( IRepository<Vehiculo> vehiculoRepository,
                                    IRepository<RegistroUbicacion> registroUbicacionesRepository,
                                    DeliveryAppContext deliveryAppContext)
        {
            _vehiculoRepository = vehiculoRepository;
            _registroUbicacionesRepository = registroUbicacionesRepository;
            _deliveryAppContext = deliveryAppContext;
        }

        /// <summary>
        /// Crea un nuevo Vehiculo
        /// </summary>
        /// <param name="nuevoVehiculo"></param>
        /// <returns></returns>
        [HttpPost("/vehiculo/crea")]
        public async Task<ActionResult<Vehiculo>> AddNewVehiculo(Vehiculo nuevoVehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _vehiculoRepository.AddAsync(nuevoVehiculo);

            return CreatedAtAction(nameof(AddNewVehiculo), new { id = nuevoVehiculo.Id }, nuevoVehiculo);
        }

        /// <summary>
        /// Muestra todos los Vehiculos existentes en la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/vehiculo/muestra-todos")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetAllVehiculos()
        {
            // var vehiculos = await _vehiculoRepository.GetAllAsync();
            var vehiculos = _deliveryAppContext.Vehiculos.Include(v => v.Envios).ToList();

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
