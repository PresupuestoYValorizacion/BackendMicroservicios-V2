using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Personas;

namespace MsAcceso.Application.Root.Users.UpdatePersona;

public sealed record UpdatePersonaCommand(
    PersonaId Id,
    ParametroId TipoId,
    ParametroId TipoDocumentoId,
    string NumeroDocumento,
    string RazonSocial,
    string NombreCompleto
) : ICommand<Guid>;