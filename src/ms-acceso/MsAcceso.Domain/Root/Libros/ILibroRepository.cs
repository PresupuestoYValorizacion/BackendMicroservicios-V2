namespace MsAcceso.Domain.Root.Libros;


public interface ILibroRepository
{
    Task<bool> LibroExist(string nombreLibro, CancellationToken cancellationToken = default);
    Task<List<Libro>> GetAllLibro(CancellationToken cancellationToken);
}