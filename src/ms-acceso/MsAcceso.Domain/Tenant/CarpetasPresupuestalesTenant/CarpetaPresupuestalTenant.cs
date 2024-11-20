using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

public sealed class CarpetaPresupuestalTenant : Entity<CarpetaPresupuestalTenantId>
{
    private CarpetaPresupuestalTenant(){}

    private CarpetaPresupuestalTenant(
        CarpetaPresupuestalTenantId id,
        CarpetaPresupuestalTenantId? dependencia,
        string nombre,
        int nivel
    ): base(id)
    {
        Dependencia = dependencia;
        Nombre = nombre;
        Nivel = nivel;
    }

    public CarpetaPresupuestalTenantId? Dependencia {get; private set;}
    public CarpetaPresupuestalTenant? DependenciaModel {get; private set;}
    public string? Nombre {get; private set;}
    public int? Nivel { get; private set; }
    public List<CarpetaPresupuestalTenant>? CarpetasPresupuestales { get; set; }


    public static CarpetaPresupuestalTenant Create(CarpetaPresupuestalTenantId? dependencia, string nombre, int nivel)
    {
        var carpetaPresupuestal = new CarpetaPresupuestalTenant(CarpetaPresupuestalTenantId.New(), dependencia, nombre, nivel);
        return carpetaPresupuestal;
    }

    public Result Update(string nombre)
    {
        Nombre = (nombre.Length > 0 ) ? nombre : Nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}
