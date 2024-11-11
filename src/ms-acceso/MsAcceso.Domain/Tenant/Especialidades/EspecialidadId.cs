namespace MsAcceso.Domain.Tenant.Especialidades;

public record EspecialidadId(Guid Value){

    public static EspecialidadId New() => new(Guid.NewGuid());
    
};