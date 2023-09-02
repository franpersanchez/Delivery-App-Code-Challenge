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

        //Será 0 por defecto hasta que se le asigne el Id de un Vehiculo
        public long VehiculoId { get; set; }
        public Vehiculo? Vehiculo { get; set; } = null!;


    }

    public enum EstadoPedido
    {
        Pendiente,
        Aceptado,
        Pagado,
        Entregado
    }
}
