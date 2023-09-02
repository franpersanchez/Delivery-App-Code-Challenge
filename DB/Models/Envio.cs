using Delivery_App_Code_Challenge.DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Envio : Entity
    {
        public long VehiculoId { get; set; }

        public string? ZonaPostal { get; set; }

        [JsonIgnore]
        public Vehiculo? Vehiculo { get; set; } = null!;

        [JsonIgnore]
        public ISet<Pedido> Pedidos { get; set; } = new HashSet<Pedido>();

    }
}
