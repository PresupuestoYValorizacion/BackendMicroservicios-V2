namespace MsAcceso.Domain.Root.RolPermisosOpciones;

public record RolPermisoOpcionId(Guid Value){
    public static RolPermisoOpcionId New() => new(Guid.NewGuid()); 
};