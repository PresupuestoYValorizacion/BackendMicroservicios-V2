using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.DeleteClienteTenant;

public sealed record DeleteClienteTenantCommand(
    ClienteTenantId Id
): ICommand<int>;