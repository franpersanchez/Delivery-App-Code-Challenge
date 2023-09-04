using DB;
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
        public DbSet<RegistroUbicacion> HistorialUbicaciones { get; set; }
        public DbSet<RegistroUbicacion> RegistroUbicaciones { get; set; }

        public DbSet<Envio> Envios { get; set; }
        public DeliveryAppContext(DbContextOptions<DeliveryAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var user1 = new Cliente { Id = 1, Nombre = "Fran", Apellidos = "Perez", Telefono = "+34 667202163", Email = "franpersanchez@gmail.com", Ubicacion_latitud = "2342N", Ubicacion_longitud = "324E" };
            var user2 = new Cliente { Id = 2, Nombre = "Marta", Apellidos = "Acedo", Telefono = "+34 665412984", Email = "martaab@gmail.com", Ubicacion_latitud = "1232132N", Ubicacion_longitud = "324E" };

            var pedido1 = new Pedido { Id = 1, ClienteId = 1, Commentarios = "Amazon, urgente!", HoraCreacion=DateTime.UtcNow, EstadoPedido = EstadoPedido.Pendiente };
            var pedido2 = new Pedido { Id = 2, ClienteId = 2, Commentarios = "El corte ingles", HoraCreacion =  DateTime.UtcNow, EstadoPedido = EstadoPedido.Pendiente };
            var pedido3 = new Pedido { Id = 3, ClienteId = 2, Commentarios = "Fnac", HoraCreacion = DateTime.UtcNow, EstadoPedido = EstadoPedido.Pendiente };

            var vehiculo1 = new Vehiculo { Id = 1, Matricula = "418GZK", NombreConductor = "Ivan Ruiz" };
            var vehiculo2 = new Vehiculo { Id = 2, Matricula = "345HJU", NombreConductor = "Lolo Sanchez" };

            modelBuilder.Entity<Cliente>().HasData(user1, user2);
            modelBuilder.Entity<Pedido>().HasData(pedido1, pedido2, pedido3);
            modelBuilder.Entity<Vehiculo>().HasData(vehiculo1, vehiculo2);

            base.OnModelCreating(modelBuilder);
        }

    }
}