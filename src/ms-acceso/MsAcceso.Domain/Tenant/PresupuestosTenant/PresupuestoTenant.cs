using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosTenant;

public sealed class PresupuestoTenant : Entity<PresupuestoTenantId>
{
    private PresupuestoTenant(){}

    private PresupuestoTenant(
        PresupuestoTenantId id,
        string codigo,
        string descripcion,
        ClienteTenantId clienteId,
        int ubigeoId,
        DateTime fecha,
        int plazodias,
        int jornadaDiariaId,
        int monedaId,
        double presupuestoBaseCD,
        double presupuestoBaseDI,
        double totalPresupuestoBase,
        double presupuestoOfertaCD,
        double presupuestoOfertaDI,
        double totalPresupuestoOferta,
        CarpetaPresupuestalTenantId carpetaPresupuestalId
        
    ): base(id)
    {
        Codigo = codigo;
        Descripcion = descripcion;
        ClienteId = clienteId;
        UbigeoId = ubigeoId;
        Fecha = fecha;
        Plazodias = plazodias;
        JornadaDiariaId = jornadaDiariaId;
        MonedaId = monedaId;
        PresupuestoBaseCD = presupuestoBaseCD;
        PresupuestoBaseDI = presupuestoBaseDI;
        TotalPresupuestoBase = totalPresupuestoBase;
        PresupuestoOfertaCD = presupuestoOfertaCD;
        PresupuestoOfertaDI = presupuestoOfertaDI;
        TotalPresupuestoOferta = totalPresupuestoOferta;
        CarpetaPresupuestalId = carpetaPresupuestalId;
    }

    public string? Codigo {get; private set;}
    public string? Descripcion {get; private set;}
    public ClienteTenant? Cliente {get; private set;}
    public ClienteTenantId? ClienteId {get; private set;}
    public int? UbigeoId {get; private set;}
    public DateTime? Fecha {get; private set;}
    public int? Plazodias {get; private set;}
    public int? JornadaDiariaId {get; private set;}
    public int? MonedaId {get; private set;}
    public double? PresupuestoBaseCD {get; private set;}
    public double? PresupuestoBaseDI {get; private set;}
    public double? TotalPresupuestoBase {get; private set;}
    public double? PresupuestoOfertaCD {get; private set;}
    public double? PresupuestoOfertaDI {get; private set;}
    public double? TotalPresupuestoOferta {get; private set;}
    public CarpetaPresupuestalTenant? CarpetaPresupuestal {get; private set;}
    
    public CarpetaPresupuestalTenantId? CarpetaPresupuestalId {get; private set;}

    public static PresupuestoTenant Create(
        string Codigo,
        string Descripcion,
        ClienteTenantId ClienteId,
        int UbigeoId,
        DateTime Fecha,
        int Plazodias,
        int JornadaDiariaId,
        int MonedaId,
        double PresupuestoBaseCD,
        double PresupuestoBaseDI,
        double TotalPresupuestoBase,
        double PresupuestoOfertaCD,
        double PresupuestoOfertaDI,
        double TotalPresupuestoOferta,
        CarpetaPresupuestalTenantId CarpetaPresupuestalId
    )
    {
        var partida = new PresupuestoTenant(PresupuestoTenantId.New(), Codigo, Descripcion, ClienteId, UbigeoId, Fecha, Plazodias, JornadaDiariaId, MonedaId, PresupuestoBaseCD, PresupuestoBaseDI, TotalPresupuestoBase, PresupuestoOfertaCD, PresupuestoOfertaDI, TotalPresupuestoOferta, CarpetaPresupuestalId);
        return partida;
    }

    public Result Update(
        string codigo,
        string descripcion,
        int ubigeoId,
        DateTime fecha,
        int plazodias,
        int jornadaDiariaId,
        int monedaId,
        double presupuestoBaseCD,
        double presupuestoBaseDI,
        double totalPresupuestoBase,
        double presupuestoOfertaCD,
        double presupuestoOfertaDI,
        double totalPresupuestoOferta
    )
    {
        Codigo = (codigo.Length > 0 ) ? codigo : Codigo;
        Descripcion = (descripcion.Length > 0 ) ? descripcion : Descripcion;
        UbigeoId = ubigeoId;
        Fecha = fecha;
        Plazodias = plazodias;
        JornadaDiariaId = jornadaDiariaId;
        MonedaId = monedaId;
        PresupuestoBaseCD = presupuestoBaseCD;
        PresupuestoBaseDI = presupuestoBaseDI;
        TotalPresupuestoBase = totalPresupuestoBase;
        PresupuestoOfertaCD = presupuestoOfertaCD;
        PresupuestoOfertaDI = presupuestoOfertaDI;
        TotalPresupuestoOferta = totalPresupuestoOferta;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}
