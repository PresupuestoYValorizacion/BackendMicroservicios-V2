using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosTenant;

public class PresupuestoTenantDto
{
    public string? Id {get;  set;}
    public string? Codigo {get;  set;}
    public string? Descripcion {get;  set;}
    public string? ClienteId {get;  set;}
    public int? DepartamentoId {get;  set;}
    public int? ProvinciaId {get;  set;}
    public int? DistritoId {get;  set;}
    public string? Fecha {get;  set;}
    public int? Plazodias {get;  set;}
    public int? JornadaDiariaId {get;  set;}
    public int? MonedaId {get;  set;}
    public double? PresupuestoBaseCD {get;  set;}
    public double? PresupuestoBaseDI {get;  set;}
    public double? TotalPresupuestoBase {get;  set;}
    public double? PresupuestoOfertaCD {get;  set;}
    public double? PresupuestoOfertaDI {get;  set;}
    public double? TotalPresupuestoOferta {get;  set;}
    public string? CarpetaPresupuestalId {get;  set;}
}