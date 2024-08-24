

using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Personas;


public static class PersonaErrors
{

    public static Error NotFound = new(
        404,
        "No existe el usuario buscado por este id"
    );

    public static Error AlreadyExists = new(
        400,
        "La persona con ese numero de documento ya existe en la base de datos"
    );



}