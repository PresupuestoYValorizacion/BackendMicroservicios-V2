
namespace MsAcceso.Domain.Root.UsuarioLicencias;
public record UsuarioLicenciaId(Guid Value){
    public static UsuarioLicenciaId New() => new(Guid.NewGuid()); 
};