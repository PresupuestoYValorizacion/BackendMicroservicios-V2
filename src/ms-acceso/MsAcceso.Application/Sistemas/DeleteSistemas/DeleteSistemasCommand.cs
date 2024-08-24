using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.DeleteSistemas;

public sealed record DeleteSistemasCommand(
    string Id
) : ICommand<Guid>;