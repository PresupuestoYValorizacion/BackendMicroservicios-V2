
namespace MsAcceso.Domain.Root.Personas;
public record PersonaId(Guid Value){
    public static PersonaId New() => new(Guid.NewGuid()); 
};