using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.GetCarpetasPresupuestales;

public sealed record GetCarpetasPresupuestalesQuery : IQuery<List<CarpetaPresupuestalTenantDto>>
{
    
}