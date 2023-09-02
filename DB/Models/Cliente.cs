using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Cliente : Entity
    {
        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string? Telefono { get; set; }

        public Ubicacion? Ubicacion { get; set; }

        List<Pedido>? Pedidos { get; set; } = null;

    }

    public class Ubicacion : Entity
    {
        public float latitud { get; set; }
        public float longitud { get; set; }
    }
}
