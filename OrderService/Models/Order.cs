using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    // Definición de la clase Order que representa un pedido.
    public class Order
    {
        // Propiedad que representa el identificador único del pedido.
        public int Id { get; set; }

        // Propiedad que representa el identificador del producto asociado al pedido.
        public int ProductId { get; set; }

        // Propiedad que representa la cantidad de productos en el pedido.
        public int Quantity { get; set; }

        // Propiedad que representa el precio total del pedido.
        public decimal TotalPrice { get; set; }

        // Propiedad que representa la fecha en que se realizó el pedido.
        public DateTime OrderDate { get; set; }
    }
}