namespace Delivery_App_Code_Challenge.DB.Models
{
    public class HistorialUbicacion : Entity
    {
        public DateTime FechaRegistro { get; set; }
        public Ubicacion Ubicacion { get; set; }

    }
}
