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