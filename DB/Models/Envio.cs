using Delivery_App_Code_Challenge.DB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Envio : Entity
    {
        [Required]
        public string? ZonaPostal { get; set; }

        [Required]
        public long VehiculoId { get; set; }

        [JsonIgnore]
        public Vehiculo? Vehiculo { get; set; } = null!;

        public ISet<Pedido> Pedidos { get; private set; } = new HashSet<Pedido>();

    }
}
