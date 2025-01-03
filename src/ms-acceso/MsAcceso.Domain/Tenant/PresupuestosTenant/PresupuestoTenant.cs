using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosTenant;

public sealed class PresupuestoTenant : Entity<PresupuestoTenantId>
{
    private PresupuestoTenant(){}

    private PresupuestoTenant(
        PresupuestoTenantId id,
        string codigo,
        string descripcion,
        ClienteTenantId clienteId,
        int departamentoId,
        int provinciaId,
        int distritoId,
        string fecha,
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
        DepartamentoId = departamentoId;
        ProvinciaId = provinciaId;
        DistritoId = distritoId;
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
    public int? DepartamentoId {get; private set;}
    public int? ProvinciaId {get; private set;}
    public int? DistritoId {get; private set;}
    public string? Fecha {get; private set;}
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
    public ProyectoTenant? ProyectoTenant {get; private set;}

    public static PresupuestoTenant Create(
        string Codigo,
        string Descripcion,
        ClienteTenantId ClienteId,
        int DepartamentoId,
        int ProvinciaId,
        int DistritoId,
        string Fecha,
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
        var presupuesto = new PresupuestoTenant(PresupuestoTenantId.New(), Codigo, Descripcion, ClienteId, DepartamentoId, ProvinciaId, DistritoId, Fecha, Plazodias, JornadaDiariaId, MonedaId, PresupuestoBaseCD, PresupuestoBaseDI, TotalPresupuestoBase, PresupuestoOfertaCD, PresupuestoOfertaDI, TotalPresupuestoOferta, CarpetaPresupuestalId);
        return presupuesto;
    }

    public Result Update(
        string codigo,
        string descripcion,
        int departamentoId,
        int provinciaId,
        int distritoId,
        string fecha,
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
        DepartamentoId = departamentoId;
        ProvinciaId = provinciaId;
        DistritoId = distritoId;
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
