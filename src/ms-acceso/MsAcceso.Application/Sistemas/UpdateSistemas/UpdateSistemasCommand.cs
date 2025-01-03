using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.UpdateSistemas;

public sealed record UpdateSistemasCommand(
    SistemaId Id,
    string? Nombre,
    string? Logo,
    string? Url
) : ICommand<Guid>;