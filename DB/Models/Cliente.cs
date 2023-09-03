using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Cliente : Entity
    {
        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Apellidos { get; set; }

        [Required]
        public string? Telefono { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string Ubicacion_latitud { get; set; }

        [Required]
        public string Ubicacion_longitud { get; set; }

        [JsonIgnore]
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    }


}
