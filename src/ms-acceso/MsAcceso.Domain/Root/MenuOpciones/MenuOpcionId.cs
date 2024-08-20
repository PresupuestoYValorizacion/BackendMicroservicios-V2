namespace MsAcceso.Domain.Root.MenuOpciones;

public record MenuOpcionId(Guid Value){
    public static MenuOpcionId New() => new(Guid.NewGuid()); 
};