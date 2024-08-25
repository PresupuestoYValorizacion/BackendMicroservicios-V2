
namespace MsAcceso.Domain.Root.Licencias;
public record LicenciaId(Guid Value){
    public static LicenciaId New() => new(Guid.NewGuid()); 
};