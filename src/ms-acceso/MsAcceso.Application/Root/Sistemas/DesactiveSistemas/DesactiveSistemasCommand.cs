using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Sistemas.DesactiveSistemas;

public sealed record DesactiveSistemasCommand(
    string Id
) : ICommand<Guid>;