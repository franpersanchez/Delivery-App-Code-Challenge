namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Pedido : Entity
    {
        public bool Entregado { get; set; } = false;

        public bool Aceptado { get; set; } = false;

        public bool Pagado { get; set; } = false;

        public string Commentarios { get; set; }


        //la ubicación se obtiene a partir de la ubicación del cliente
        public long ClienteId { get; set; }

        public Cliente Cliente { get; set; }


    }
}
