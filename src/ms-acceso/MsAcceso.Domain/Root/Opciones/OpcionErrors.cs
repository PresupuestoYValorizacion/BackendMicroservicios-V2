using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Opciones;

public static class OpcionErrors
{
    public static Error NotFound = new Error(
        404,
        "No se encontro la opcion buscada"
    );

    public static Error OpcionExists = new(
        400,
        "El nombre de esta opcion ya existe"
    );
    public static Error OpcionInUse = new(
        400,
        "La opcion esta en uso no puede eliminarlo"
    );
}