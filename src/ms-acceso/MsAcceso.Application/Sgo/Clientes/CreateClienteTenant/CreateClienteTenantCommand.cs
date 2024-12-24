using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Clientes.CreateClienteTenant;

public sealed record CreateClienteTenantCommand(
    int TipoPersonaId,
    int TipoDocumentoId,
    int TipoClienteId,
    string NumeroDocumento,
    string Nombre
) : ICommand<Guid>;