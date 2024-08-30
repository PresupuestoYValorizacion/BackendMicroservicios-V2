namespace MsAcceso.Domain.Root.Productos;
public record ProductoId(Guid Value){
    public static ProductoId New() => new(Guid.NewGuid()); 
};