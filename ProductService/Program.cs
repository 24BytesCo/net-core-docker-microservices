using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios de la aplicación.
builder.Services.AddEndpointsApiExplorer(); // Habilita la exploración de endpoints de API para Swagger.
builder.Services.AddSwaggerGen(); // Agrega y configura el generador de Swagger para la documentación de la API.

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Establece la cultura por defecto para las solicitudes.
    // En este caso, se usa inglés de Estados Unidos (en-US).
    options.DefaultRequestCulture = new RequestCulture("en-US");

    // Define las culturas (idiomas) soportadas por la aplicación.
    // Esto determina qué culturas se aceptan en las solicitudes entrantes.
    // Solo se soporta inglés de Estados Unidos (en-US).
    options.SupportedCultures = new[] { new CultureInfo("en-US") };

    // Define las culturas de la interfaz de usuario (idiomas) soportadas por la aplicación.
    // Esto afecta a la traducción de recursos y mensajes mostrados al usuario.
    // También se configura para inglés de Estados Unidos (en-US).
    options.SupportedUICultures = new[] { new CultureInfo("en-US") };
});

//Confugurando la cadena de conexión
builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers(); // Agrega controladores a la aplicación.

var app = builder.Build(); // Construye la aplicación web.

// Este bloque de código se ejecuta durante el inicio de la aplicación
// y se encarga de aplicar las migraciones de Entity Framework Core a la base de datos.

// Crea un nuevo ámbito de servicios. 
// Los ámbitos controlan el ciclo de vida de los objetos y las dependencias.
// El ámbito se libera automáticamente al finalizar el bloque 'using', lo que es importante para 
// evitar fugas de memoria y otros problemas.
using (var scope = app.Services.CreateScope()) 
{
    // Obtiene el IServiceProvider del ámbito actual.
    // El IServiceProvider permite acceder a los servicios registrados.
    var services = scope.ServiceProvider; 

    // ProductContext representa la conexión a la base de datos y contiene las configuraciones de Entity Framework Core.
    // GetRequiredService lanza una excepción si el servicio no está registrado.
    // Obtiene una instancia del contexto de la base de datos ProductContext.
    var context = services.GetRequiredService<ProductContext>(); 


    // Las migraciones son archivos de código que describen cómo modificar la  estructura de la base de datos (crear tablas, agregar columnas, etc.).
    // Migrate() aplica las migraciones que aún no se han aplicado, asegurando que la base de datos tenga el esquema correcto según el modelo.
    // Aplica las migraciones pendientes a la base de datos.
    context.Database.Migrate(); 
                               
} 



app.UseRequestLocalization(); // Habilita la localización de solicitudes.

app.UseAuthorization(); // Habilita la autorización en la aplicación.

// Configura el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    // Si el entorno es de desarrollo, habilita Swagger y Swagger UI.
    app.UseSwagger(); // Habilita el middleware de Swagger para servir la documentación de la API.
    app.UseSwaggerUI(); // Habilita el middleware de Swagger UI para visualizar y probar la API.
}

app.UseHttpsRedirection(); // Redirecciona las solicitudes HTTP a HTTPS.

app.Run();