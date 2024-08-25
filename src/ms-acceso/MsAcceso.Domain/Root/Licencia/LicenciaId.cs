
namespace MsAcceso.Domain.Root.Licencia;
public record LicenciaId(Guid Value){
    public static LicenciaId New() => new(Guid.NewGuid()); 
};