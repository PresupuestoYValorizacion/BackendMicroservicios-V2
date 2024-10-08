namespace MsAcceso.Application.Root.Libros.RegisterLibros;

public record RegisterLibroRequest(
    string Nombre,
    string Descripcion,
    double Precio
);