# Utiliza la imagen base de SDK de .NET 8.0 para la etapa de construcción (build).
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establece el directorio de trabajo dentro del contenedor.
WORKDIR /src

# Deshabilita la globalización invariable para que la aplicación utilice la cultura del sistema.
# Esto es importante para que la aplicación maneje correctamente las diferentes culturas e idiomas.
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIAN = false

# Copia el archivo del proyecto ProductService.csproj y la carpeta ProductService al directorio /src.
# Esto permite que el comando 'dotnet restore' restaure las dependencias del proyecto.
COPY ["ProductService.csproj", "ProductService/"]

# Restaura las dependencias del proyecto ProductService.csproj.
RUN dotnet restore "ProductService/ProductService.csproj"

# Copia el resto del código fuente al directorio /src/ProductService.
COPY . ./ProductService

# Establece el directorio de trabajo dentro del contenedor para la etapa de construcción.
WORKDIR "/src/ProductService"

# Construye el proyecto ProductService.csproj en modo Release y guarda la salida en /app/build.
RUN dotnet build "ProductService.csproj" -c Release -o /app/build

# Utiliza una nueva imagen base para la etapa de publicación (publish).
# Se basa en la imagen de construcción (build).
FROM build AS publish

# Publica el proyecto ProductService.csproj en modo Release y guarda la salida en /app/publish.
RUN dotnet publish "ProductService.csproj" -c Release -o /app/publish

# Utiliza la imagen base de ASP.NET Core 8.0 para la etapa final (final).
# Esta imagen es más ligera que la imagen de SDK, ya que solo contiene el runtime de ASP.NET Core.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Establece el directorio de trabajo dentro del contenedor para la etapa final.
WORKDIR /app

# Copia los archivos publicados desde la etapa de publicación (publish) al directorio /app.
COPY --from=publish /app/publish .

# Define el punto de entrada del contenedor.
# Se ejecuta el archivo ProductService.dll con el comando 'dotnet'.
ENTRYPOINT ["dotnet", "ProductService.dll"]