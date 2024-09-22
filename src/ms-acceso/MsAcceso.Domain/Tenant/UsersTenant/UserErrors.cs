

using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.UsersTenant;


public static class UserTenantErrors
{

    public static Error NotFound = new(
        404,
        "No existe el usuario buscado por este id"
    );

    public static Error InvalidCredentials = new(
        403,
        "Las credenciales son incorrectas"
    );

    public static Error AlreadyExists = new(
        400,
        "El usuario ya existe en la base de datos"
    );

    public static Error EmailExists = new(
        400,
        "El email ya existe en la base de datos"
    );
    public static Error EmpresaNotExists = new(
        400,
        "La empresa no existe en la base de datos"
    );
    public static Error UserInUse = new(
        400,
        "El usuario esta en uso no puede eliminarlo."
    );


}