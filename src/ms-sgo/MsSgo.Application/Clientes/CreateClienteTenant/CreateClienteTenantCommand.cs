using MsAcceso.Application.Abstractions.Messaging;

namespace MsSgo.Application.Clientes.CreateClienteTenant;

public sealed record CreateClienteTenantCommand(
    int TipoPersonaId,
    int TipoDocumentoId,
    string NumeroDocumento,
    string Nombre
) : ICommand<Guid>;