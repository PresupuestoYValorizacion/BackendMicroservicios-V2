using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.DeleteCarpetaPresupuestal;

public sealed record DeleteCarpetaPresupuestalCommand(
    CarpetaPresupuestalTenantId Id
): ICommand<Guid>;