using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    /// <summary>
    /// Representa un producto en el sistema.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// Campo es obligatorio.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Precio del producto.
        /// </summary>
        public decimal Price { get; set; }
        
    }
}