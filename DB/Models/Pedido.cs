using System.ComponentModel;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Pedido : Entity
    {
        public EstadoPedido EstadoPedido { get; set; } = EstadoPedido.Pendiente;

        public string? Commentarios { get; set; }


        //la ubicación (destino) se obtiene a partir de la ubicación del cliente
        public long ClienteId { get; set; }

        public Cliente? Cliente { get; set; } = null!;

        public DateTime HoraCreacion { get; set; }

    }

    public enum EstadoPedido
    {
        Pendiente,
        Aceptado,
        Pagado,
        Entregado
    }
}
