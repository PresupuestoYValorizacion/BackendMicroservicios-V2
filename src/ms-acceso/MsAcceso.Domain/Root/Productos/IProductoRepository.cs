

namespace MsAcceso.Domain.Root.Productos;

public interface IProductoRepository
{
    void Add(Producto producto);

    void Update(Producto producto);
    
    void Delete(Producto producto);

    Task<List<Producto>> GetAll(CancellationToken cancellationToken = default);

    Task<bool> ProductoExists(string productoNombre, CancellationToken cancellationToken = default);
    
    Task<string> GetLastCodigoAsync(CancellationToken cancellationToken = default);

    Task<Producto?> GetByIdAsync(ProductoId productoId, CancellationToken cancellationToken);

    Task<Producto?> GetByNombre(string nombre, CancellationToken cancellationToken = default);

}