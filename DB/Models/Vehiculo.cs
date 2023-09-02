using DB;

namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Vehiculo : Entity
    {
        public string? Matricula { get; set; }

        public string? NombreConductor { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; } = new List<Pedido>(); 

        public ICollection<RegistroUbicacion> RegistroUbicaciones {  get; set; } = new List<RegistroUbicacion>();
    }

}
