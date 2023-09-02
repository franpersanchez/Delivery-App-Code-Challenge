using DB.Models;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Vehiculo : Entity
    {
        public string? Matricula { get; set; }

        public string? NombreConductor { get; set; }

        public List<Pedido>? Pedidos { get; set; } = null!; 
    }

}
