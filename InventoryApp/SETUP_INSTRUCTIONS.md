# Instrucciones de Configuración - Base de Datos SQLite

## Resumen de Cambios

Se ha configurado exitosamente la aplicación InventoryApp para utilizar SQLite como base de datos persistente. A continuación se detallan los cambios realizados:

## 1. Dependencias Agregadas

### Archivo: `InventoryApp.csproj`

Se agregó el paquete NuGet de Entity Framework Core para SQLite:

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.5" />
```

**Versión**: 10.0.5 (Compatible con .NET 10.0)

## 2. Configuración de la Base de Datos

### Archivo: `MauiProgram.cs`

Se modificó la configuración del DbContext para usar SQLite en lugar de base de datos en memoria:

**Antes:**
```csharp
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseInMemoryDatabase("InventoryDb"),
    ServiceLifetime.Singleton);
```

**Después:**
```csharp
// Configurar la ruta de la base de datos SQLite
var dbPath = Path.Combine(FileSystem.AppDataDirectory, "InventoryApp.db");

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlite($"Data Source={dbPath}"),
    ServiceLifetime.Singleton);
```

## 3. Inicializador de Base de Datos

### Archivo Nuevo: `Data/DatabaseInitializer.cs`

Se creó una clase responsable de:
- Crear la base de datos si no existe
- Crear las tablas basadas en los modelos
- Llenar con datos iniciales (seed data)

```csharp
public static async Task InitializeAsync(AppDbContext context)
{
    // Crear la base de datos si no existe
    await context.Database.EnsureCreatedAsync();
    
    // Seed de datos...
}
```

## 4. Estructura de la Base de Datos

### Tablas Creadas

#### 1. **Products**
- Id (GUID - Clave Primaria)
- Nombre (Texto)
- Descripcion (Texto)
- Precio (Decimal)
- Stock (Entero)
- Activo (Booleano)
- FechaCreacion (Fecha/Hora)

#### 2. **Proveedores**
- Id (GUID - Clave Primaria)
- Foto (Texto)
- Nombre (Texto)
- TipoProducto (Texto)
- Telefono (Texto)
- Email (Texto)
- Activo (Booleano)

## 5. Modelos Existentes

Los siguientes modelos fueron utilizados para generar las tablas de la base de datos (sin cambios):

- `InventoryApp/Models/Product.cs`
- `InventoryApp/Models/Proveedor.cs`

## 6. Capas de Acceso a Datos

### Repositorios (Sin cambios)
- `InventoryApp/Repositories/IProductRepository.cs`
- `InventoryApp/Repositories/ProductRepository.cs`
- `InventoryApp/Repositories/IProveedorRepository.cs`
- `InventoryApp/Repositories/ProveedorRepository.cs`

### Servicios (Sin cambios)
- `InventoryApp/Services/ProductService.cs`
- `InventoryApp/Services/ProveedorService.cs`

## 7. Ubicación de la Base de Datos

La base de datos SQLite se almacena en:

```
{AppDataDirectory}/InventoryApp.db
```

Ubicaciones específicas por plataforma:

| Plataforma | Ubicación |
|-----------|-----------|
| Windows | `%APPDATA%/InventoryApp/InventoryApp.db` |
| Android | `/data/data/com.companyname.inventoryapp/files/InventoryApp.db` |
| iOS | `Library/Application Support/InventoryApp/InventoryApp.db` |
| macOS | `~/Library/Application Support/InventoryApp/InventoryApp.db` |

## 8. Flujo de Inicialización

1. La aplicación inicia
2. El contenedor de inyección de dependencias se configura
3. Se crea una instancia de `AppDbContext` con la cadena de conexión SQLite
4. Se llama a `DatabaseInitializer.InitializeAsync()` 
5. Se crea la base de datos si no existe
6. Se crean las tablas según los modelos de Entity Framework
7. Se insertan datos iniciales si las tablas están vacías
8. La aplicación está lista para funcionar

## 9. Operaciones Disponibles

### Obtener Todos los Productos
```csharp
var products = await productService.GetProducts();
```

### Obtener Producto por ID
```csharp
var product = await productService.GetById(productId);
```

### Crear Nuevo Producto
```csharp
var newProduct = new Product 
{ 
    Nombre = "Nuevo Producto",
    Descripcion = "Descripción",
    Precio = 100m,
    Stock = 5
};
await productService.Create(newProduct);
```

### Actualizar Producto
```csharp
product.Stock = 20;
await productService.Update(product);
```

### Desactivar Producto
```csharp
await productService.Disable(product);
```

## 10. Datos Iniciales (Seed)

La base de datos se llena automáticamente con datos de prueba:

### Productos
1. Laptop Dell XPS 15 - $3500.00 (10 unidades)
2. Mouse inalámbrico Logitech - $80000.00 (50 unidades)
3. Teclado mecánico RGB - $150000.00 (30 unidades)
4. Monitor 27" 4K UHD - $800.00 (5 unidades)

### Proveedores
1. TechDistribuciones S.A. - Electrónica
2. OfficeSupplies Ltda. - Papelería y Oficina
3. Hardware Pro - Componentes de Computadora
4. MegaImport Corp. - Periféricos

## 11. Compilación y Prueba

Para compilar la aplicación:

```bash
cd InventoryApp
dotnet build
```

Para ejecutar la aplicación (según la plataforma):

```bash
# Windows
dotnet run -f net10.0-windows10.0.19041.0

# Android
dotnet build -f net10.0-android -c Debug

# iOS Simulator
dotnet build -f net10.0-ios -c Debug
```

## 12. Resolución de Problemas

### La base de datos se corrompe
**Solución**: Elimina el archivo `InventoryApp.db` y reinicia la aplicación.

### Los datos no persisten
**Verificar**:
1. Que `SaveChangesAsync()` se ejecute correctamente
2. Que haya permisos de escritura en el directorio de datos

### Error de conexión a SQLite
**Verificar**:
1. Que `Microsoft.EntityFrameworkCore.Sqlite` esté instalado
2. Que la ruta de la base de datos sea válida
3. Que haya permisos de lectura/escritura

## 13. Archivos Nuevos/Modificados

### Archivos Nuevos
- ✅ `InventoryApp/Data/DatabaseInitializer.cs`
- ✅ `InventoryApp/Data/DATABASE_SCHEMA.md`
- ✅ `InventoryApp/SETUP_INSTRUCTIONS.md` (este archivo)

### Archivos Modificados
- ✅ `InventoryApp/InventoryApp.csproj` (agregó referencia a SQLite)
- ✅ `InventoryApp/MauiProgram.cs` (configuración de SQLite)

### Archivos Sin Cambios (Compatibles)
- ✅ `InventoryApp/Data/AppDbContext.cs`
- ✅ `InventoryApp/Data/DbContextFactory.cs`
- ✅ `InventoryApp/Models/Product.cs`
- ✅ `InventoryApp/Models/Proveedor.cs`
- ✅ `InventoryApp/Repositories/*`
- ✅ `InventoryApp/Services/*`
- ✅ `InventoryApp/ViewModels/*`
- ✅ `InventoryApp/Views/*`

## 14. Tecnologías Utilizadas

- **Framework**: .NET 10.0 MAUI
- **Base de Datos**: SQLite
- **ORM**: Entity Framework Core 10.0.5
- **Patrón de Arquitectura**: Repository Pattern
- **Inyección de Dependencias**: Microsoft.Extensions.DependencyInjection

## 15. Próximos Pasos (Opcional)

Para mejorar la configuración en el futuro, considera:

1. **Migraciones EF Core**: Implementar migraciones para versionamiento de esquema
2. **Auditoría**: Agregar campos como `UsuarioCreacion`, `FechaModificacion`, etc.
3. **Validaciones**: Agregar Data Annotations y validaciones en los modelos
4. **Relaciones**: Crear relaciones entre Productos y Proveedores
5. **Backup**: Implementar funcionalidad de backup de base de datos
6. **Encriptación**: Cifrar datos sensibles en la base de datos

---

**Fecha de Configuración**: 2026-02-05
**Versión**: 1.0
**Estado**: ✅ Completado