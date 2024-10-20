using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Users.RegisterUsersTenant;

public sealed record RegisterUsersTenantCommand(
    string Email, 
    string Username, 
    string Password,
    int TipoId,
    int TipoDocumentoId,
    string NumeroDocumento,
    string RazonSocial,
    string NombreCompleto,
    RolTenantId RolId
) : ICommand<Guid>;