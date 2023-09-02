using DB.Interfaces;
using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_App_Code_Challenge.Controllers
{
    [ApiController]
    [Route("[controller")]
    public class UbicacionVehiculosController : ControllerBase
    {
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly IRepository<RegistroUbicacion> _registroUbicacionRepository;

        public UbicacionVehiculosController(IRepository<Vehiculo> vehiculoRepository,
                                            IRepository<RegistroUbicacion> registroUbicacionRepository
                                           )
        {
            _vehiculoRepository = vehiculoRepository;
            _registroUbicacionRepository = registroUbicacionRepository;

        }

        [HttpPut("/localization/vehicle-update-position")]
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

        [HttpGet("/localization-history/vehicle/{vehicle_id}")]
        public async Task<ActionResult<IEnumerable<RegistroUbicacion>>> GetRegistroUbicacionForVehicle(long vehicle_id)
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
                    return registroUbicacion.ToList();
                }
                else
                {
                    return NotFound("No RegistroUbicaciones for this Vehiculo yet");
                }
            }
        }

        [HttpGet("/current-localization/vehicle/{vehicle_id}")]
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
    }
}
