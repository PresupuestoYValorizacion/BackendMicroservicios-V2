
using MsAcceso.Domain.Entity;

namespace MsAcceso.Domain.Repository;

public interface IProductRepository
{

    Task<Product?> GetByIdAsync(ProductId id, CancellationToken cancellationToken = default);

    void Add(Product user);

    void Update(Product user);
    
    void Delete(Product user);

    

}