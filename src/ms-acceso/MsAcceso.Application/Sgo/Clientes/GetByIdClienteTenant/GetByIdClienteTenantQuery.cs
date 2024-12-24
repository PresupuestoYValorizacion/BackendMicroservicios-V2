using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetByIdClienteTenant;

public sealed record GetByIdClientTenantQuery : IQuery<ClienteDto>
{
    public string Id { get; set; } = string.Empty;
}