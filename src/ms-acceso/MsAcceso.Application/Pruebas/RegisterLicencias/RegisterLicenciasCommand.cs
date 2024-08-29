using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Pruebas.RegisterLicencias;

public sealed record RegisterLicenciasCommand(
    string Nombre
) : ICommand<Guid>;