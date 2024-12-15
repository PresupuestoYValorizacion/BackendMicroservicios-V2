using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Tenant.PartidasTenant;

public static class PartidaTenantErrors
{
    public static Error PartidaNotFound = new(400, "Esta partida no existe");
    public static Error PartidaIntercambioNotFound = new(400, "La partida para intercambiar no existe");
    public static Error PartidaNotAvailable = new(400, "Esta partida no existe, o esta desactivada");
    public static Error PartidaNameExists = new(400, "Este nombre de partida ya existe, ingrese otro.");
    public static Error PartidaInUse = new(400, "La partida esta en uso no puede eliminarla.");
}