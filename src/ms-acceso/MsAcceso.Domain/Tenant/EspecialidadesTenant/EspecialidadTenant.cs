using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;
using MsAcceso.Domain.Tenant.PresupuestosTenant;

namespace MsAcceso.Domain.Tenant.EspecialidadesTenant;

public sealed class EspecialidadTenant : Entity<EspecialidadTenantId>
{
    private EspecialidadTenant(){}

    private EspecialidadTenant(
        EspecialidadTenantId id,
        string nombre
    ) : base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre { get; private set; }
    public List<PresupuestoTenant>? Presupuestos { get; } = [];
    public List<PresupuestoEspecialidadTenant>? PresupuestosEspecialidades { get; } = [];

    public static EspecialidadTenant Create(
        string Nombre
    )
    {
        var especialidad = new EspecialidadTenant(EspecialidadTenantId.New(), Nombre);
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