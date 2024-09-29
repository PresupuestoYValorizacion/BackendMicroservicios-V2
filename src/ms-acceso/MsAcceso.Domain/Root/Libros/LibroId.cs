namespace MsAcceso.Domain.Root.Libros;

public record LibroId(Guid Value)
{
    public static LibroId New() => new(Guid.NewGuid());
    
};