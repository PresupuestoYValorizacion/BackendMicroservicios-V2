using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sistemas.RegisterSistemas;

public sealed record RegisterSistemasCommand(
    string Nombre,
    string Logo,
    string Url,
    string? Dependecia
): ICommand<Guid>;