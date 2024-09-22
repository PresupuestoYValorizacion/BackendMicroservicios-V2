namespace MsAcceso.Domain.Tenant.Users;

public record UserId(Guid Value){
    public static UserId New() => new(Guid.NewGuid()); 
};