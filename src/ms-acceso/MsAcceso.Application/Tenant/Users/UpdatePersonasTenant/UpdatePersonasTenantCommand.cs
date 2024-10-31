using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Tenant.PersonasTenant;

namespace MsAcceso.Application.Root.Users.UpdatePersona;

public sealed record UpdatePersonasTenantCommand(
    PersonaTenantId Id,
    // ParametroId TipoId,
    // ParametroId TipoDocumentoId,
    int TipoId,
    int TipoDocumentoId,
    string NumeroDocumento,
    string RazonSocial,
    string NombreCompleto
) : ICommand<Guid>;