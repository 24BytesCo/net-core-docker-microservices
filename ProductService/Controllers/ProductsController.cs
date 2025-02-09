using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Controllers
{
    // [ApiController] indica que este controlador está diseñado para servir respuestas de API.
    // Proporciona características como validación automática y manejo de errores mejorado.
    [ApiController]

    // [Route("api/[controller]")] define la ruta base para este controlador.
    // [controller] se reemplaza dinámicamente con el nombre del controlador (en este caso, "products"). Por lo tanto, la ruta base será "api/products".
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // [HttpGet("GetAllProducts")] define una acción que responde a solicitudes GET a la ruta "api/products/GetAllProducts".
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            // Usa el contexto de la base de datos para obtener todos los productos de forma asíncrona.
            return await _context.Products.ToListAsync();
        }

        // [HttpGet("GetProduct/{id}")] define una acción que responde a solicitudes GET a la ruta "api/products/GetProduct/{id}".
        // {id} es un parámetro de ruta que se captura y se pasa al método GetProduct.
        [HttpGet("GetProduct/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // Busca un producto por su ID de forma asíncrona.
            var product = await _context.Products.FindAsync(id);

            // Si no se encuentra el producto, devuelve un resultado NotFound (código de estado 404).
            if (product == null)
            {
                return NotFound();
            }

            // Si se encuentra el producto, lo devuelve.
            return product;
        }

        // [HttpPost("PostProduct")] define una acción que responde a solicitudes POST a la ruta "api/products/PostProduct".
        [HttpPost("PostProduct")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Agrega el nuevo producto al contexto de la base de datos.
            _context.Products.Add(product);
            // Guarda los cambios en la base de datos de forma asíncrona.
            await _context.SaveChangesAsync();

            // Devuelve un resultado CreatedAtAction (código de estado 201) con la información del producto creado.
            // "GetProduct" es el nombre de la acción que se utilizará para generar la URL del recurso creado.
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // [HttpPut("PutProduct/{id}")] define una acción que responde a solicitudes PUT a la ruta "api/products/PutProduct/{id}".
        [HttpPut("PutProduct/{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            // Si el ID del producto en la URL no coincide con el ID del producto en el cuerpo de la solicitud,
            // devuelve un resultado BadRequest (código de estado 400).
            if (id != product.Id)
            {
                return BadRequest();
            }

            // Marca la entidad product como modificada en el contexto de la base de datos.
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                // Guarda los cambios en la base de datos de forma asíncrona.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // Maneja excepciones de concurrencia.
            {
                if (!ProductExists(id)) // Si el producto no existe, devuelve NotFound.
                {
                    return NotFound();
                }
                else
                {
                    throw; // Re-lanza la excepción si no es por un producto inexistente.
                }
            }

            // Devuelve un resultado CreatedAtAction (código de estado 201) con la información del producto actualizado.
            // "GetProduct" es el nombre de la acción que se utilizará para generar la URL del recurso actualizado.

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // [HttpDelete("DeleteProduct/{id}")] define una acción que responde a solicitudes DELETE a la ruta "api/products/DeleteProduct/{id}".
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // Busca un producto por su ID de forma asíncrona.
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Si no se encuentra el producto, devuelve NotFound.
            }

            // Elimina el producto del contexto de la base de datos.
            _context.Products.Remove(product);
            // Guarda los cambios en la base de datos de forma asíncrona.
            await _context.SaveChangesAsync();

            return Ok("Product deleted successfully"); // Devuelve un resultado Ok (código de estado 200) con un mensaje de éxito.
        }

        // Método privado para verificar si un producto existe en la base de datos.
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}