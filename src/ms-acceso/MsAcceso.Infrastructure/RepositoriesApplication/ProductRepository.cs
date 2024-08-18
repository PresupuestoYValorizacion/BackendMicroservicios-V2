
using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Repository;

namespace MsAcceso.Infrastructure.RepositoriesApplication;

internal sealed class ProductRepository : RepositoryApplication<Product,ProductId>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}