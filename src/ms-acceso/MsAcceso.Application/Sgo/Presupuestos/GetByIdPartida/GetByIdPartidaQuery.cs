using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetByIdPartida;

public sealed record GetByIdPartidaQuery : IQuery<PartidaTenantDto>
{
    public string Id { get; set; } = string.Empty;
}