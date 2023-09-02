using Delivery_App_Code_Challenge.DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DefaultValue(false)]
        public bool Entregado { get; set; } = false;
    }
}
