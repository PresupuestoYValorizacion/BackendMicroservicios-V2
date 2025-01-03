using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Infrastructure.Extensions;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.RepositoriesTenant;


internal abstract class RepositoryTenant<TEntity, TEntityId>
where TEntity : Entity<TEntityId>
where TEntityId : class
{
    protected readonly TenantDbContext DbContext;

    protected RepositoryTenant(TenantDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TEntityId id,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<TEntity>()
        .FirstOrDefaultAsync(x => x.Id == id && x.Activo == new Activo(true), cancellationToken);
    }


    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        DbContext.Remove(entity);
    }


    public async Task<PagedResults<TEntity, TEntityId>> GetPaginationAsync
      (
          Expression<Func<TEntity, bool>>? predicate,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
          int page,
          int pageSize,
          string orderBy,
          bool ascending,
          bool disableTracking = true
      )
    {

        IQueryable<TEntity> queryable = DbContext.Set<TEntity>();

        if (disableTracking) queryable = queryable.AsNoTracking();

        if (predicate is not null)
        {
            queryable = queryable.Where(predicate);
        }

        if (includes is not null)
        {
            queryable = includes(queryable);
        }
        var skipAmount = pageSize * (page - 1);

        var totalNumberOfRecords = await queryable.CountAsync();

        var records = new List<TEntity>();

        if (string.IsNullOrEmpty(orderBy))
        {
            records = await queryable.Skip(skipAmount).Take(pageSize).ToListAsync();
        }
        else
        {
            records = await queryable.OrderByPropertyOrField(orderBy, ascending).Skip(skipAmount).Take(pageSize).ToListAsync();

        }

        var mod = totalNumberOfRecords % pageSize;

        var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

        return new PagedResults<TEntity, TEntityId>
        {
            Results = records,
            PageNumber = page,
            PageSize = pageSize,
            TotalNumberOfPages = totalPageCount,
            TotalNumberOfRecords = totalNumberOfRecords
        };

    }

}



internal abstract class RepositoryTenant<TEntity>
where TEntity : class
{
    protected readonly TenantDbContext DbContext;

    protected RepositoryTenant(TenantDbContext dbContext)
    {
        DbContext = dbContext;
    }


    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        DbContext.Remove(entity);
    }
    




}
