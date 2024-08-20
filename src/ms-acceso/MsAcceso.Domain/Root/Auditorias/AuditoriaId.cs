namespace MsAcceso.Domain.Root.Auditorias;

public record AuditoriaId(Guid Value){
    public static AuditoriaId New() => new(Guid.NewGuid()); 
};