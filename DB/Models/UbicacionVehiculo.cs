using Delivery_App_Code_Challenge.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class UbicacionVehiculo
    {
        public long VehiculoId { get; set; }
        private Vehiculo? Vehiculo { get; set; } = null!;
        public DateTime FechaUltimaUbicacion { get; set; }

        private Ubicacion _UbicacionActual;

        public Ubicacion UbicacionActual
        {
            get { return _UbicacionActual; }
            set
            {
                //if UbicacionActual is different than the one being passed
                if (_UbicacionActual != value)
                {
                    //if UbicacionActual already contains a value, that value is passed to history
                    if (_UbicacionActual != null)
                    {
                        AgregarUbicacionAlHistorial(_UbicacionActual);
                    }

                    _UbicacionActual = value;
                }
            }
        }

        private List<HistorialUbicacion>? historialUbicaciones { get; set; }

        // Método para agregar una ubicación al historial
        private void AgregarUbicacionAlHistorial(Ubicacion ubicacion)
        {
            var historialUbicacion = new HistorialUbicacion
            {
                FechaRegistro = FechaUltimaUbicacion,
                Ubicacion = ubicacion
            };
            historialUbicaciones.Add(historialUbicacion);
        }
    }
}

