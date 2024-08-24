using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Sistemas;

public static class SistemaErrors
{
    public static Error SistemaNotFound = new(400, "Este sistema no existe");
    public static Error SistemaNameExists = new(400, "Este nombre de sistema ya existe, eliga otro.");
}