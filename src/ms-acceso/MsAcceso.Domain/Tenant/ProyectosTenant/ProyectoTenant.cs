using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;

namespace MsAcceso.Domain.Tenant.ProyectosTenant;

public sealed class ProyectoTenant : Entity<ProyectoTenantId>
{
    private ProyectoTenant(){}

    private ProyectoTenant(
        ProyectoTenantId id,
        string nombre
    ): base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre {get; private set;}
    public List<EspecialidadTenant>? Especialidades { get; } = [];
    

    public static ProyectoTenant Create(
        string Nombre
    )
    {
        var proyecto = new ProyectoTenant(ProyectoTenantId.New(),Nombre);
        return proyecto;
    }

    public Result Update(
        string nombre
    )
    {
        Nombre = nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}
