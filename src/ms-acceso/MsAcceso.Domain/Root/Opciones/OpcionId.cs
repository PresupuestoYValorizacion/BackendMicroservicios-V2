namespace MsAcceso.Domain.Root.Opciones;

public record OpcionId(Guid Value){

    public static OpcionId New() => new(Guid.NewGuid());
};