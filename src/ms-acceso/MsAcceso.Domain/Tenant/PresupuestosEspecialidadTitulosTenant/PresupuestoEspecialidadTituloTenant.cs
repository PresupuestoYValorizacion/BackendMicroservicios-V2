using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

public sealed class PresupuestoEspecialidadTituloTenant : Entity<PresupuestoEspecialidadTituloTenantId>
{
    private PresupuestoEspecialidadTituloTenant(){}

    private PresupuestoEspecialidadTituloTenant(
        PresupuestoEspecialidadTituloTenantId id,
        PresupuestoEspecialidadTenantId presupuestoEspecialidadId,
        TituloTenantId tituloId,
        PresupuestoEspecialidadTituloTenantId? dependencia,
        int nivel,
        string correlativo
    ) : base(id)
    {
        PresupuestoEspecialidadId = presupuestoEspecialidadId;
        TituloId = tituloId;
        Dependencia = dependencia;
        Nivel = nivel;
        Correlativo = correlativo;
    }

    public PresupuestoEspecialidadTenantId? PresupuestoEspecialidadId {get; private set;}
    public PresupuestoEspecialidadTenant? PresupuestoEspecialidad {get; private set;}
    public TituloTenantId?  TituloId {get; private set;}
    public TituloTenant? Titulo {get; private set;}
    public PresupuestoEspecialidadTituloTenantId? Dependencia {get; private set;}
    public PresupuestoEspecialidadTituloTenant? DependenciaModel {get; private set;}
    public int? Nivel { get; private set; }
    public string? Correlativo { get; private set; }
    public List<PresupuestoEspecialidadTituloTenant>? PresupuestosEspecialidadTitulos { get; set; }

    public static PresupuestoEspecialidadTituloTenant Create(
        PresupuestoEspecialidadTituloTenantId Id,
        PresupuestoEspecialidadTenantId PresupuestoEspecialidadId,
        TituloTenantId TituloId,
        PresupuestoEspecialidadTituloTenantId? Dependencia,
        int Nivel,
        string Correlativo  
    )
    {
        var presupuestoEspecialidadTitulo = new PresupuestoEspecialidadTituloTenant(Id, PresupuestoEspecialidadId, TituloId, Dependencia, Nivel, Correlativo);
        return presupuestoEspecialidadTitulo;
    }

    public Result Update(
        PresupuestoEspecialidadTenantId presupuestoEspecialidadId,
        TituloTenantId tituloId,
        string correlativo
    )
    {
        PresupuestoEspecialidadId = presupuestoEspecialidadId;
        TituloId = tituloId;
        Correlativo = (correlativo.Length > 0) ? correlativo : Correlativo;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    } 
}