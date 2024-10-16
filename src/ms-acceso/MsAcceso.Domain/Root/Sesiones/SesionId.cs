namespace MsAcceso.Domain.Root.Sesiones;

public record SesionId(Guid Value){
    public static SesionId New() => new(Guid.NewGuid()); 
};