using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using OrderService.Data;
using OrderService.Models; 

namespace OrderService.Controllers
{
    // Controlador de API para la gestión de órdenes.
    [ApiController] // Indica que es un controlador de API
    [Route("api/[controller]")] // Define la ruta base para el controlador

    public class OrderController : ControllerBase 
    {
        private readonly OrderContext _context; // Contexto de la base de datos

        // Constructor que recibe el contexto de la base de datos (inyección de dependencias).
        public OrderController(OrderContext context)
        {
            _context = context;
        }

        // Obtiene todas las órdenes.
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // Obtiene una orden por su ID.
        [HttpGet("GetOrder/{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la orden
            }

            return order;
        }

        // Crea una nueva orden.
        [HttpPost("PostOrder")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order); // Devuelve 201 Created
        }

        // Actualiza una orden existente.
        [HttpPut("PutOrder/{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(); // Devuelve 400 Bad Request si los IDs no coinciden
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // Maneja excepciones de concurrencia
            {
                if (!OrderExists(id))
                {
                    return NotFound(); // Devuelve 404 si la orden no existe
                }
                else
                {
                    throw; // Re-lanza la excepción
                }
            }

            return Ok("Updated successfully"); // Devuelve 200 Ok con un mensaje de éxito
        }

        // Elimina una orden.
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound(); // Devuelve 404 si no se encuentra la orden
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully"); // Devuelve 200 Ok con un mensaje de éxito
        }

        // Verifica si una orden existe en la base de datos.
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}