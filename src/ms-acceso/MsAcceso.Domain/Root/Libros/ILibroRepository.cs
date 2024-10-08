namespace MsAcceso.Domain.Root.Libros;


public interface ILibroRepository
{
    void Add(Libro libro);
    Task<bool> LibroExist(string nombreLibro, CancellationToken cancellationToken = default);
    Task<List<Libro>> GetAllLibro(CancellationToken cancellationToken);
}