using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Domain.Tenant.RecursosTenant;

public sealed class RecursoTenant : Entity<RecursoTenantId>
{
    private RecursoTenant(){}

    private RecursoTenant(
        RecursoTenantId id,
        string nombre,
        int tipoRecursoId,
        int unidadMedidaId
    ) : base(id)
    {
        Nombre = nombre;
        TipoRecursoId = tipoRecursoId;
        UnidadMedidaId = unidadMedidaId;
    }

    public string? Nombre { get; private set; }
    public int? TipoRecursoId { get; private set; }
    public int UnidadMedidaId { get; private set; }
    // public List<PartidaTenant>? Partidas { get; } = [];
    // public List<PartidaRecursoTenant>? PartidasRecursos { get; } = [];

    public static RecursoTenant Create(
        string Nombre,
        int TipoRecursoId,
        int UnidadMedidaId
    )
    {
        var recurso = new RecursoTenant(RecursoTenantId.New(), Nombre, TipoRecursoId, UnidadMedidaId);
        return recurso;
    }

    public Result Update(
        string nombre,
        int tipoRecursoId,
        int unidadMedidaId
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        TipoRecursoId = (tipoRecursoId > 0) ? tipoRecursoId : TipoRecursoId;
        UnidadMedidaId = (unidadMedidaId > 0) ? unidadMedidaId : UnidadMedidaId;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}