
using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Rols;

public static class RolErrors
{
    public static Error NotFound = new(
        404,
        "No existe el rol buscado por este id"
    );

    public static Error AlreadyExists = new(
        400,
        "El rol ya existe en la base de datos"
    );

    public static Error RolNotExists = new(
        400,
        "El rol no existe en la base de datos"
    );
    public static Error RolInUse = new(
        400,
        "El rol esta en uso no puede eliminarlo"
    );
}