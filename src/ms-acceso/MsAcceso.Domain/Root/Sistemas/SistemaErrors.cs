using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Sistemas;

public static class SistemaErrors
{
    public static Error SistemaNotFound = new(400, "Este sistema no existe");
    public static Error SistemaIntercambioNotFound = new(400, "El sistema para intercambiar no existe");
    public static Error SistemaNotAvailable = new(400, "Este sistema no existe, o esta desactivado");
    public static Error SistemaNameExists = new(400, "Este nombre de sistema ya existe, ingrese otro.");
    public static Error SistemaUrlExists = new(400, "Este url de sistema ya existe, ingrese otro.");
    public static Error SistemaInUse = new(400, "El menu esta en uso no puede eliminarlo.");
}