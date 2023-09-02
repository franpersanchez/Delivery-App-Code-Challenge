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

        /// <summary>
        /// Actualiza la posición dada de un Vehiculo pasando un nuevo objeto RegistroUbicacion
        /// </summary>
        /// <param name="nuevaUbicacion"></param>
        /// <returns></returns>
        [HttpPut("/localizacion/vehiculo/actualiza-posicion")]
        public async Task<IActionResult> UpdateUbicacion([FromBody] RegistroUbicacion nuevaUbicacion)
        {
            try
            {
                var vehiculo = await _vehiculoRepository.GetSingleOrDefaultAsync(v => v.Id == nuevaUbicacion.VehiculoId);

                if (vehiculo == null)
                {
                    return NotFound("Vehículo asociado a la ubicación no encontrado");
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


        /// <summary>
        /// Muestra una lista del histórico de Ubicaciones de un Vehiculo determinado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/localizacion-historico/vehiculo/{id}")]
        public async Task<ActionResult<IEnumerable<RegistroUbicacion>>> GetRegistroUbicacionForVehicle(long id)
        {
            var vehicle = await _vehiculoRepository.GetSingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound("Vehiculo no encontrado");
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
                    return NotFound("Sin Registro de Ubicaciones para este vehículo aún");
                }
            }
        }

        /// <summary>
        /// Muestra el resultado más reciente del histórico de Ubicaciones de un Vehiculo determinado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/localizacion-actual/vehiculo/{id}")]
        public async Task<ActionResult<IEnumerable<RegistroUbicacion>>> GetCurrentRegistroUbicacionForVehicle(long id)
        {
            var vehicle = await _vehiculoRepository.GetSingleOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound("Vehiculo no encontrado en la base de datos");
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
                    return NotFound("Sin Registro de Ubicaciones para este vehículo aún");
                }
            }
        }
    }
}
