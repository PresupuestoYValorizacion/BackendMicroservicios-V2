
namespace MsAcceso.Domain.Tenant.PersonasTenant;
public record PersonaTenantId(Guid Value){
    public static PersonaTenantId New() => new(Guid.NewGuid()); 
};