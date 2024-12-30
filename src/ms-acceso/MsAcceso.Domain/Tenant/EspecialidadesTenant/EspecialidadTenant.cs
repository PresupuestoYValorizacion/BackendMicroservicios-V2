using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Domain.Tenant.EspecialidadesTenant;

public sealed class EspecialidadTenant : Entity<EspecialidadTenantId>
{
    private EspecialidadTenant(){}

    private EspecialidadTenant(
        EspecialidadTenantId id,
        ProyectoTenantId proyectoTenantId,
        string nombre
    ) : base(id)
    {
        Nombre = nombre;
        ProyectoTenantId = proyectoTenantId;
    }

    public string? Nombre { get; private set; }
    public ProyectoTenantId? ProyectoTenantId { get; private set; }
    public ProyectoTenant? ProyectoTenant { get; private set; }
    public List<PresupuestoTenant>? Presupuestos { get; } = [];
    public List<PresupuestoEspecialidadTenant>? PresupuestosEspecialidades { get; } = [];

    public static EspecialidadTenant Create(
        string Nombre,
        ProyectoTenantId ProyectoTenantId
    )
    {
        var especialidad = new EspecialidadTenant(EspecialidadTenantId.New(),ProyectoTenantId, Nombre);
        return especialidad;
    }

    public Result Update(
        string nombre
    )
    {
        Nombre = (nombre.Length > 0) ? nombre : Nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}