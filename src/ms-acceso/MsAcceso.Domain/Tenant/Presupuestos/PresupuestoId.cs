namespace MsAcceso.Domain.Tenant.Presupuestos;

public record PresupuestoId(Guid Value){
    public static PresupuestoId New() => new(Guid.NewGuid()); 
};