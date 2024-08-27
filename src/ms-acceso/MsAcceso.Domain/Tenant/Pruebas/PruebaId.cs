namespace MsAcceso.Domain.Tenant.Pruebas;

public record PruebaId(Guid Value){
    public static PruebaId New() => new(Guid.NewGuid()); 
};