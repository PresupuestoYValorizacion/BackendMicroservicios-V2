using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Tenant.TitulosTenant;

public sealed class TituloTenant : Entity<TituloTenantId>
{
    private TituloTenant(){}

    private TituloTenant(
        TituloTenantId id,
        string nombre
    ) : base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre { get; private set; }

    public static TituloTenant Create(
        string Nombre
    )
    {
        var titulo = new TituloTenant(TituloTenantId.New(), Nombre);
        return titulo;
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