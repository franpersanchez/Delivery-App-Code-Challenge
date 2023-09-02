using Delivery_App_Code_Challenge.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class UbicacionVehiculo : Entity
    {
        public long VehiculoId { get; set; }
        private Vehiculo? Vehiculo { get; set; } = null!;
        public DateTime FechaUltimaUbicacion { get; set; }

        private UbicacionVehiculoCoordenadas _UbicacionVehiculoCoordenadas;

        public UbicacionVehiculoCoordenadas UbicacionVehiculoCoordenadas
        {
            get { return _UbicacionVehiculoCoordenadas; }
            set
            {
                //if UbicacionActual is different than the one being passed
                if (_UbicacionVehiculoCoordenadas != value)
                {
                    //if UbicacionActual already contains a value, that value is passed to history
                    if (_UbicacionVehiculoCoordenadas != null)
                    {
                        AgregarUbicacionAlHistorial(_UbicacionVehiculoCoordenadas);
                    }

                    _UbicacionVehiculoCoordenadas = value;
                }
            }
        }

        private List<HistorialUbicacion>? historialUbicaciones { get; set; }

        // Método para agregar una ubicación al historial
        private void AgregarUbicacionAlHistorial(UbicacionVehiculoCoordenadas ubicacion)
        {
            var historialUbicacion = new HistorialUbicacion
            {
                FechaRegistro = FechaUltimaUbicacion,
                UbicacionVehiculoCoordenadas = ubicacion
            };
            historialUbicaciones.Add(historialUbicacion);
        }
    }

    public class UbicacionVehiculoCoordenadas : Entity
    {
        public float latitud { get; set; }
        public float longitud { get; set; }

    }

}

