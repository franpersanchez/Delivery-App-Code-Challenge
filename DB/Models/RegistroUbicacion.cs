using DB;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class RegistroUbicacion : Entity
    {
        [Required]
        public DateTime FechaRegistro { get; set; }

        [Required]
        public string Ubicacion_longitud {  get; set; }

        [Required]
        public string Ubicacion_latitud { get; set; }

        [Required]
        public long VehiculoId { get; set; }

        public Vehiculo Vehiculo { get; set; } = null!;

    }
}
