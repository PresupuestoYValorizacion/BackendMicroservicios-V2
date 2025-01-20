using System.ComponentModel.DataAnnotations.Schema;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;

namespace MsAcceso.Domain.Tenant.RecursosTenant;

public sealed class RecursoTenant : Entity<RecursoTenantId>
{
    private RecursoTenant(){}

    private RecursoTenant(
        RecursoTenantId id,
        string nombre,
        int unidadMedidaId,
        int tipoRecursoId
    ) : base(id)
    {
        Nombre = nombre;
        TipoRecursoId = tipoRecursoId;
        UnidadMedidaId = unidadMedidaId;
    }

    public string? Nombre { get; private set; }
    public int UnidadMedidaId { get; private set; }
    public int TipoRecursoId {get; private set;}

    [NotMapped]
    public Parametro? TipoRecurso { get; set; }

    [NotMapped]
    public Parametro? UnidadMedida { get; set; }
    public List<PresupuestoEspecialidadTituloPartidaTenant>? PresupuestosEspecialidadesTitulosPartidas { get; } = [];
    public List<PresupuestoEspecialidadTituloPartidaRecursoTenant> PresupuestosEspecialidadesTitulosPartidasRecursos { get; } = [];


    public static RecursoTenant Create(
        string Nombre,
        int UnidadMedidaId,
        int TipoRecursoId
    )
    {
        var recurso = new RecursoTenant(RecursoTenantId.New(), Nombre, UnidadMedidaId, TipoRecursoId);
        return recurso;
    }

    public Result Update(
        string nombre,
        int unidadMedidaId
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        UnidadMedidaId = (unidadMedidaId > 0) ? unidadMedidaId : UnidadMedidaId;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}