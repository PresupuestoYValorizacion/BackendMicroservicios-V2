namespace MsAcceso.Domain.Root.Users;

public record UserId(Guid Value){
    public static UserId New() => new(Guid.NewGuid()); 
};