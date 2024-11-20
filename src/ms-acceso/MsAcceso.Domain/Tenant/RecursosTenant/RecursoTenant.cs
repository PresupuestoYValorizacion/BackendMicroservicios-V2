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
        int idTipoRecurso,
        int idUnidadMedida
    ) : base(id)
    {
        Nombre = nombre;
        IdTipoRecurso = idTipoRecurso;
        IdUnidadMedida = idUnidadMedida;
    }

    public string? Nombre { get; private set; }
    public int? IdTipoRecurso { get; private set; }
    public int IdUnidadMedida { get; private set; }
    // public List<PartidaTenant>? Partidas { get; } = [];
    // public List<PartidaRecursoTenant>? PartidasRecursos { get; } = [];

    public static RecursoTenant Create(
        string Nombre,
        int IdTipoRecurso,
        int IdUnidadMedida
    )
    {
        var recurso = new RecursoTenant(RecursoTenantId.New(), Nombre, IdTipoRecurso, IdUnidadMedida);
        return recurso;
    }

    public Result Update(
        string nombre,
        int idTipoRecurso,
        int idUnidadMedida
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        IdTipoRecurso = (idTipoRecurso > 0) ? idTipoRecurso : IdTipoRecurso;
        IdUnidadMedida = (idUnidadMedida > 0) ? idUnidadMedida : IdUnidadMedida;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}