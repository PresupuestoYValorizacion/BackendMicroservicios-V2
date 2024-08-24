using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.DesactiveSistemas;

public sealed record DesactiveSistemasCommand(
    string Id
) : ICommand<Guid>;