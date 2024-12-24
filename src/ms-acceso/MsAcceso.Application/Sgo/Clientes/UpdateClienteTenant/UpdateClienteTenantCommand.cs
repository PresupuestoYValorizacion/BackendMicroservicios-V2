using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Clientes.UpdateClienteTenant;

public sealed record UpdateClienteTenantCommand(
    string Id,
    int TipoPersonaId,
    int TipoDocumentoId,
    int TipoClienteId,
    string NumeroDocumento,
    string Nombre
): ICommand<Guid>;