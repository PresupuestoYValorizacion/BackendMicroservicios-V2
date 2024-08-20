namespace MsAcceso.Domain.Root.Sistemas;
public record SistemaId(Guid Value){
    public static SistemaId New() => new(Guid.NewGuid()); 
};