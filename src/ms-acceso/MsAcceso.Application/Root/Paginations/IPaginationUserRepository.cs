using System.Linq.Expressions;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;
using Microsoft.EntityFrameworkCore.Query;

namespace MsAcceso.Application.Root.Paginations;

public interface IPaginationUserRepository
{
    Task<PagedResults<User, UserId>> GetPaginationAsync(
        Expression<Func<User,bool>>? predicate,
        Func<IQueryable<User>, IIncludableQueryable<User,object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    );
}