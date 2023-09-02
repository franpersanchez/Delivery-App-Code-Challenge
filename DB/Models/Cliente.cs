using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Cliente : Entity
    {
        public string? Nombre { get; set; }

        public string? Apellidos { get; set; }

        public string? telefono { get; set; }

        public string? ubicacion { get; set; }

        public List<Pedido> Pedidos { get; set; }

    }
}
