using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.GetByIdCarpetaPresupuestal;

public sealed record GetByIdCarpetaPresupuestalQuery : IQuery<CarpetaPresupuestalTenantDto>
{
    public string Id { get; set; } = string.Empty;
}