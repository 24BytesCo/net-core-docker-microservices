using Microsoft.EntityFrameworkCore;
using ProductService.Models;

namespace ProductService.Data
{
    // Define la clase ProductContext que hereda de DbContext.
    // ProductContext representa el contexto de la base de datos para la aplicación.
    public class ProductContext : DbContext
    {
        // Constructor de ProductContext que recibe DbContextOptions<ProductContext>.
        // Estas opciones se utilizan para configurar el contexto de la base de datos, como la cadena de conexión y el proveedor de base de datos.
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        // Define un DbSet<Product> llamado Products.
        // DbSet representa una tabla en la base de datos y se utiliza para realizar 
        // consultas y operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la tabla Product.
        public DbSet<Product> Products { get; set; }

        // Sobrescribe el método OnModelCreating para configurar el modelo de la base de datos.
        // Este método se llama durante la creación del modelo y permite definir 
        // configuraciones específicas para las entidades y las relaciones.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura la propiedad Price de la entidad Product.
            // HasColumnType("decimal(18,2)") especifica que la columna correspondiente  en la base de datos debe ser de tipo decimal con una precisión de 18 dígitos 
            // y 2 decimales. Esto es importante para garantizar la precisión de los valores monetarios.
            modelBuilder.Entity<Product>().
                Property(p => p.Price).
                HasColumnType("decimal(18,2)");

            // Llama al método base.OnModelCreating para asegurarse de que se ejecuten las configuraciones predeterminadas.
            base.OnModelCreating(modelBuilder);
            
        }
    }
}