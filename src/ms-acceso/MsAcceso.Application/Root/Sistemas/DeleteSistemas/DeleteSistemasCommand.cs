using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Sistemas.DeleteSistemas;

public sealed record DeleteSistemasCommand(
    string Id
) : ICommand<Guid>;