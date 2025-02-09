using Microsoft.EntityFrameworkCore; 
using OrderService.Models;

namespace OrderService.Data
{
    // Clase que representa el contexto de la base de datos para la aplicación OrderService.
    // Hereda de DbContext, proporcionando la funcionalidad para interactuar con la base de datos.
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        // Propiedad DbSet que representa la tabla de Orders en la base de datos.
        // Permite realizar consultas y operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la tabla Orders.
        public DbSet<Order> Orders { get; set; }

        // Método OnModelCreating para configurar el modelo de la base de datos.
        // Se llama durante la creación del modelo y permite personalizar la forma en que las entidades se mapean a las tablas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura la propiedad TotalPrice de la entidad Order para que tenga un tipo decimal(18,2) en la base de datos.
            // Esto asegura que los precios se almacenen con la precisión necesaria.
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Llama al método OnModelCreating de la clase base (DbContext).
            // Esto es importante para que se apliquen las configuraciones predeterminadas del modelo.
            base.OnModelCreating(modelBuilder);
        }
    }
}