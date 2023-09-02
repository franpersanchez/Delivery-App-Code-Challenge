using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Cliente : Entity
    {
        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string? Telefono { get; set; }

        public float latitud { get; set; }
        public float longitud { get; set; }

        List<Pedido>? Pedidos { get; set; } = null;

    }


}
