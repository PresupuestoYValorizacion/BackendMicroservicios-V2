namespace MsAcceso.Domain.Root.Resenias;
public record ReseniaId(Guid Value){
    public static ReseniaId New() => new(Guid.NewGuid()); 
};