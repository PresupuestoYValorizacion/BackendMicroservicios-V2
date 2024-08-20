using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Domain.Root.Auditorias;

public sealed class Auditoria : Entity<AuditoriaId>
{
    private Auditoria(){}

    private Auditoria(
        AuditoriaId id,
        string tabla,
        string campo,
        int accion,
        string valorAnterior,
        string valorActual,
        DateTime fecha,
        UserId userId
    ) : base(id)
    {

    }

    public string? Tabla { get; private set; }
    public string? Campo{ get; private set; }
    public int? Accion { get; private set; }
    public string? ValorAnterior { get; private set; }
    public string? ValorActual { get; private set; }
    public DateTime? Fecha { get; private set; }
    public UserId? UserId { get; private set; }
}