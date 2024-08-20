namespace MsAcceso.Domain.Root.EmpresasSistemas;

public record EmpresaSistemaId(Guid Value){
    public static EmpresaSistemaId New() => new(Guid.NewGuid()); 
};