using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Sistemas.RegisterSistemas;

public sealed record RegisterSistemasCommand(
    string Nombre,
    string Logo,
    string Url,
    string? Dependecia,
    int Nivel
): ICommand<Guid>;