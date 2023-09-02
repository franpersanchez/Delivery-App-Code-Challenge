using System.ComponentModel;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Pedido : Entity
    {
        [DefaultValue(false)]
        public bool Entregado { get; set; } = false;
        [DefaultValue(false)]
        public bool Aceptado { get; set; } = false;
        [DefaultValue(false)]
        public bool Pagado { get; set; } = false;

        public string? Commentarios { get; set; }


        //la ubicación (destino) se obtiene a partir de la ubicación del cliente
        public long ClienteId { get; set; }

        Cliente? Cliente { get; set; } = null;

        public DateTime HoraCreacion { get; set; }


    }
}
