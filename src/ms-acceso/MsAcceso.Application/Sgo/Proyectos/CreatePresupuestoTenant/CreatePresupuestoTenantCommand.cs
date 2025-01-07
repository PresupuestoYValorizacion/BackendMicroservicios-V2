using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Proyectos.CreatePresupuestoTenant;

public sealed record CreatePresupuestoTenantCommand(
    string Codigo,
    string Descripcion,
    string ClienteId,
    int DepartamentoId,
    int ProvinciaId,
    int DistritoId,
    string Fecha,
    int Plazodias,
    int JornadaDiariaId,
    int MonedaId,
    double PresupuestoBaseCD,
    double PresupuestoBaseCI,
    double TotalPresupuestoBase,
    double PresupuestoOfertaCD,
    double PresupuestoOfertaCI,
    double TotalPresupuestoOferta,
    string CarpetaPresupuestalId,
    string ProyectoId
) : ICommand<Guid>;