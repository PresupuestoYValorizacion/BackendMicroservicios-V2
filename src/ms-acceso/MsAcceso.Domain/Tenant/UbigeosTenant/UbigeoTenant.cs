using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Tenant.UbigeosTenant;

public sealed class UbigeoTenant : Entity<UbigeoTenantId>
{
    private UbigeoTenant(){}

    private UbigeoTenant(
        UbigeoTenantId id,
        UbigeoTenantId? dependencia,
        string nombre,
        int nivel
    ): base(id)
    {
        Dependencia = dependencia;
        Nombre = nombre;
        Nivel = nivel;
    }

    public UbigeoTenantId? Dependencia {get; private set;}
    public UbigeoTenant? DependenciaModel {get; private set;}
    public string? Nombre {get; private set;}
    public int? Nivel { get; private set; }
    public List<UbigeoTenant>? Ubigeos { get; set; }


    public static UbigeoTenant Create(UbigeoTenantId? dependencia, string nombre, int nivel)
    {
        var sistema = new UbigeoTenant(UbigeoTenantId.New(), dependencia, nombre, nivel);
        return sistema;
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
