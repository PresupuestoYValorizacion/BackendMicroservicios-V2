using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Libros;

public static class LibroErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro el libro buscado"
    );
    public static Error LibroExists = new(
        400,
        "El nombre de este libro ya existe"
    );
}