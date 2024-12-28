namespace MsAcceso.Domain.Tenant.ProyectosTenant;

public record ProyectoTenantId(Guid Value)
{
    public static ProyectoTenantId New() => new(Guid.NewGuid()); 
};