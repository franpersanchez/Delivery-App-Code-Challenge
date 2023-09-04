using DB;
using DB.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Vehiculo : Entity
    {
        [Required]
        public string? Matricula { get; set; }

        [Required]
        public string? NombreConductor { get; set; }

        [ReadOnly(true)]
        public ICollection<Envio>? Envios { get; set; } = new List<Envio>();

        
        public ICollection<RegistroUbicacion> RegistroUbicaciones {  get; set; } = new List<RegistroUbicacion>();
    }

}
