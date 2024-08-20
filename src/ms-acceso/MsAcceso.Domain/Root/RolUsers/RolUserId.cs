namespace MsAcceso.Domain.Root.RolUsers;

public record RolUserId(Guid Value){
    public static RolUserId New() => new(Guid.NewGuid()); 
};