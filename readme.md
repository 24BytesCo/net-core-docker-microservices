# Microservicios de Gestión de Pedidos y Productos

Este repositorio alberga dos microservicios básicos: OrderService y ProductService. Ambos han sido construidos con .NET 8 y utilizan Entity Framework Core para la gestión de datos. Una característica clave es su dockerización, lo que facilita su despliegue y portabilidad, con cada servicio corriendo en un puerto distinto para un aislamiento óptimo.

## OrderService

Microservicio encargado de la gestión de pedidos.

### Funcionalidades

*   API con rutas descriptivas.
*   Configuración de Docker y docker-compose.
*   Aplicación de migraciones de Entity Framework Core al inicio.
*   Configuración de localización y base de datos.
*   CRUD básico de productos y pedidos con Entity Framework Core.
*   Soporte para SQL Server.

### Tecnologías

*   .NET 8
*   Entity Framework Core
*   SQL Server

## ProductService

Microservicio encargado de la gestión de productos.

### Funcionalidades

*   API con rutas descriptivas.
*   Configuración de Docker y docker-compose.
*   Aplicación de migraciones de Entity Framework Core al inicio.
*   Configuración de localización, base de datos y Swagger.
*   CRUD básico de productos con Entity Framework Core.
*   Soporte para SQL Server.

### Tecnologías

*   .NET 8
*   Entity Framework Core
*   SQL Server
*   Docker

### Paquetes NuGet

*   `Microsoft.AspNetCore.OpenApi`
*   `Microsoft.EntityFrameworkCore.Design`
*   `Microsoft.EntityFrameworkCore.SqlServer`
*   `Microsoft.EntityFrameworkCore.Tools`

## Configuración

1.  Clonar el repositorio.
2.  Configurar las cadenas de conexión a la base de datos en ambos proyectos.
3.  Ejecutar las migraciones de Entity Framework Core en ProductService y OrderService.
4.  Construir y ejecutar los proyectos.

## Docker

El proyecto incluye configuración de Docker y docker-compose para el servicio de productos y de órdenes.

## Notas

*   El proyecto ProductService fue migrado de .NET 9 a .NET 8.
*   Se han añadido comentarios para explicar decisiones de código.
