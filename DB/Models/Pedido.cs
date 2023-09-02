using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Pedido : Entity
    {
        [JsonIgnore]
        public EstadoPedido EstadoPedido { get; set; } = EstadoPedido.Pendiente;

        public string? Commentarios { get; set; }


        //la ubicación (destino) se obtiene a partir de la ubicación del cliente
        public long ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; } = null!;

        public DateTime HoraCreacion { get; set; }        

    }

    public enum EstadoPedido
    {
        Pendiente,
        Pagado,
        Aceptado,
        Enviado,
        Entregado
    }
}
