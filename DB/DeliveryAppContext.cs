using DB.Models;
using Delivery_App_Code_Challenge.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class DeliveryAppContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<HistorialUbicacion> HistorialUbicaciones { get; set; }
        public DbSet<UbicacionVehiculo> UbicacionesVehiculo { get; set; }

        public DeliveryAppContext(DbContextOptions<DeliveryAppContext> options) : base(options)
        {
        }



    }
}