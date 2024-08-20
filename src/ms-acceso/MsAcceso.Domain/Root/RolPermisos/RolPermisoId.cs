namespace MsAcceso.Domain.Root.RolPermisos;

public record RolPermisoId(Guid Value){
    public static RolPermisoId New() => new(Guid.NewGuid()); 
};