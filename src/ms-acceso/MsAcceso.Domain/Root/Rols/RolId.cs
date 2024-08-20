namespace MsAcceso.Domain.Root.Rols;
public record RolId(Guid Value){
    public static RolId New() => new(Guid.NewGuid()); 
};