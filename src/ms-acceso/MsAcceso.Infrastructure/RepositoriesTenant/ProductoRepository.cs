
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class ProductoRepository : RepositoryTenant<Producto, ProductoId>, IProductoRepository
{
    public ProductoRepository(TenantDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Producto>> GetAll(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Producto>().Where(x => x.Activo == new Activo(true)).ToListAsync(cancellationToken);
    }

    public async Task<bool> ProductoExists(string productoNombre, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Producto>().AnyAsync(x => x.Nombre == productoNombre, cancellationToken);
    }

    public async Task<string> GetLastCodigoAsync(CancellationToken cancellationToken = default)
    {
        var lastCodigo = await DbContext.Set<Producto>()
                                .OrderByDescending(p => p.Codigo)
                                .FirstOrDefaultAsync(cancellationToken);
        return lastCodigo!.Codigo!;
    }

    public async Task<Producto?> GetByNombre(string nombre, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Producto>().Where(x => x.Nombre == nombre).FirstOrDefaultAsync(cancellationToken);
    }

}