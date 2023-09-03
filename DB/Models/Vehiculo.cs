using DB;
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

        [JsonIgnore]
        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>();

        [JsonIgnore]
        public ICollection<RegistroUbicacion> RegistroUbicaciones {  get; set; } = new List<RegistroUbicacion>();
    }

}
