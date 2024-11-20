using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

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