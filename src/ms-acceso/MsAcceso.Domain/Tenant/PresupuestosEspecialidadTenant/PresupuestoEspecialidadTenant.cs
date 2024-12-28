using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;

public sealed class PresupuestoEspecialidadTenant : Entity<PresupuestoEspecialidadTenantId>
{
    private PresupuestoEspecialidadTenant(){}

    private PresupuestoEspecialidadTenant(
        PresupuestoEspecialidadTenantId id,
        PresupuestoTenantId presupuestoId,
        EspecialidadTenantId especialidadId,
        string correlativo
    ) : base(id)
    {
        PresupuestoId = presupuestoId;
        EspecialidadId = especialidadId;
        Correlativo = correlativo;
    }

    public PresupuestoTenantId? PresupuestoId { get; private set; }
    public EspecialidadTenantId? EspecialidadId { get; private set; }
    public ProyectoTenantId? ProyectoId { get; private set; }
    public PresupuestoTenant? Presupuesto { get; private set; }
    public EspecialidadTenant? Especialidad { get; private set; }
    public ProyectoTenant? Proyecto { get; private set; }
    public string? Correlativo { get; private set; }

    public static PresupuestoEspecialidadTenant Create(
        PresupuestoEspecialidadTenantId Id,
        PresupuestoTenantId PresupuestoId,
        EspecialidadTenantId EspecialidadId,
        string Correlativo
    )
    {
        var presupuestoEspecialidad = new PresupuestoEspecialidadTenant(Id, PresupuestoId, EspecialidadId, Correlativo);
        return presupuestoEspecialidad;
    }

    public Result Update(
        PresupuestoTenantId presupuestoId,
        EspecialidadTenantId especialidadId,
        string correlativo
    )
    {
        PresupuestoId = presupuestoId;
        EspecialidadId = especialidadId;
        Correlativo = (correlativo.Length > 0) ? correlativo : Correlativo;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    } 
}