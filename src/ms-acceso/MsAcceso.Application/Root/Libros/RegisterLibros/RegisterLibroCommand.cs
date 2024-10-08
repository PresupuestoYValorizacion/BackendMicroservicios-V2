using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Libros.RegisterLibros;

public sealed record RegisterLibroCommand(
    string Nombre, 
    string Descripcion,
    double Precio
): ICommand<Guid>;