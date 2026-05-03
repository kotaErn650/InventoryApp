# Esquema de Base de Datos - InventoryApp

## Descripción General

La aplicación InventoryApp utiliza una base de datos SQLite para persistir datos de productos y proveedores. La base de datos se configura automáticamente al iniciar la aplicación.

## Ubicación de la Base de Datos

- **Ruta**: `{AppDataDirectory}/InventoryApp.db`
- **Tipo**: SQLite
- **Ubicación del archivo**:
  - **Android**: `/data/data/com.companyname.inventoryapp/files/InventoryApp.db`
  - **iOS**: `Application Support/InventoryApp.db`
  - **Windows**: `%APPDATA%/InventoryApp/InventoryApp.db`

## Tablas

### 1. Tabla: Products

Almacena la información de los productos del inventario.

| Columna | Tipo | Restricciones | Descripción |
|---------|------|---------------|-------------|
| Id | TEXT (GUID) | PRIMARY KEY | Identificador único del producto |
| Nombre | TEXT | NOT NULL | Nombre del producto |
| Descripcion | TEXT | NOT NULL | Descripción detallada del producto |
| Precio | DECIMAL | NOT NULL | Precio unitario del producto |
| Stock | INTEGER | NOT NULL | Cantidad disponible en inventario |
| Activo | BOOLEAN | NOT NULL | Estado del producto (activo/inactivo) |
| FechaCreacion | DATETIME | NOT NULL | Fecha y hora de creación del registro |

**Ejemplo de datos**:
```
Id: 550e8400-e29b-41d4-a716-446655440000
Nombre: Laptop
Descripcion: Laptop Dell XPS 15
Precio: 3500.00
Stock: 10
Activo: 1 (true)
FechaCreacion: 2026-02-05 19:57:00
```

### 2. Tabla: Proveedores

Almacena la información de los proveedores de productos.

| Columna | Tipo | Restricciones | Descripción |
|---------|------|---------------|-------------|
| Id | TEXT (GUID) | PRIMARY KEY | Identificador único del proveedor |
| Foto | TEXT | NOT NULL | Ruta o nombre de la imagen del proveedor |
| Nombre | TEXT | NOT NULL | Nombre del proveedor |
| TipoProducto | TEXT | NOT NULL | Tipo de productos que suministra |
| Telefono | TEXT | NOT NULL | Número de teléfono de contacto |
| Email | TEXT | NOT NULL | Correo electrónico de contacto |
| Activo | BOOLEAN | NOT NULL | Estado del proveedor (activo/inactivo) |

**Ejemplo de datos**:
```
Id: 660e8400-e29b-41d4-a716-446655440001
Foto: hard.png
Nombre: TechDistribuciones S.A.
TipoProducto: Electrónica
Telefono: 3212222
Email: aajdha@Ecci.edu.co
Activo: 1 (true)
```

## Relaciones

No hay relaciones explícitas de clave foránea en el esquema actual. Los productos y proveedores se almacenan de manera independiente.

## Inicialización de Datos

La base de datos se inicializa automáticamente con datos de prueba la primera vez que se ejecuta la aplicación:

### Productos iniciales:
1. Laptop Dell XPS 15 - $3500.00 (10 unidades)
2. Mouse inalámbrico Logitech - $80000.00 (50 unidades)
3. Teclado mecánico RGB - $150000.00 (30 unidades)
4. Monitor 27" 4K UHD - $800.00 (5 unidades)

### Proveedores iniciales:
1. TechDistribuciones S.A. - Electrónica
2. OfficeSupplies Ltda. - Papelería y Oficina
3. Hardware Pro - Componentes de Computadora
4. MegaImport Corp. - Periféricos

## Configuración en la Aplicación

### Archivo: MauiProgram.cs

```csharp
// Configurar la ruta de la base de datos SQLite
var dbPath = Path.Combine(FileSystem.AppDataDirectory, "InventoryApp.db");

builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlite($"Data Source={dbPath}"),
    ServiceLifetime.Singleton);
```

### Archivo: AppDbContext.cs

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
```

### Archivo: DatabaseInitializer.cs

Responsable de:
1. Crear la base de datos si no existe
2. Crear las tablas según los modelos
3. Llenar con datos iniciales si las tablas están vacías

## Acceso a la Base de Datos

### A través de Repositorios

```csharp
// Inyección de dependencias
private readonly IProductRepository _productRepository;

// Métodos disponibles
var products = await _productRepository.GetAll();
var product = await _productRepository.GetById(id);
await _productRepository.Add(newProduct);
await _productRepository.Update(existingProduct);
```

### A través de Servicios

```csharp
private readonly ProductService _productService;

// Métodos disponibles
var products = await _productService.GetProducts();
var product = await _productService.GetById(id);
await _productService.Create(newProduct);
await _productService.Update(existingProduct);
await _productService.Disable(product);
```

## Notas Importantes

1. **Sincronización de cambios**: Los cambios en la base de datos se guardan automáticamente mediante `SaveChangesAsync()`.

2. **Entity Framework Core**: La aplicación utiliza Entity Framework Core con SQLite como proveedor de base de datos.

3. **Portabilidad**: El archivo de base de datos SQLite se almacena en el directorio de datos de la aplicación, lo que lo hace portátil y específico del usuario.

4. **Asincronía**: Todas las operaciones de base de datos son asincrónicas para evitar bloqueos de UI.

5. **Ciclo de vida**: La base de datos se inicializa automáticamente en el startup de la aplicación.

## Troubleshooting

Si experiencias problemas con la base de datos:

1. **Base de datos corrupta**: Elimina el archivo `InventoryApp.db` y reinicia la aplicación.
2. **Datos no persisten**: Verifica que la operación `SaveChangesAsync()` se ejecute correctamente.
3. **Problemas de permisos**: Asegúrate de que la aplicación tenga permisos de lectura/escritura en el directorio de datos.