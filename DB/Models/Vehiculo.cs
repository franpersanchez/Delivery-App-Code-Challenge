namespace Delivery_App_Code_Challenge.DB.Models
{
    public class Vehiculo : Entity
    {
        public string Matricula { get; set; }
        public string UbicacionActual { get; set; }

        public List<HistorialUbicacion> HistorialUbicaciones { get; set; }
    }
}
