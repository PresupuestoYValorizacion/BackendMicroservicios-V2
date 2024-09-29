using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Libros;

namespace MsAcceso.Application.Root.Paginations;

public interface IPaginationLibroRepository
{
    Task<PagedResults<Libro, LibroId>> GetPaginationAsync(
        Expression<Func<Libro, bool>> predicate,
        Func<IQueryable<Libro>, IIncludableQueryable<Libro, object>> includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    );
}