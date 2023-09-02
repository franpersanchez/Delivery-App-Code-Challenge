using Delivery_App_Code_Challenge.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Envio : Entity
    {
        public long VehiculoId { get; set; }

        public Vehiculo? Vehiculo { get; set; } = null!;

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public bool Entregado { get; set; } = false;
    }
}
