using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.MenuOpcions.RegisterMenuOpcion;

public sealed record RegisterMenuOpcionCommand(
    OpcionId OpcionId,
    SistemaId SistemaId,
    bool TieneUrl,
    string Url
) : ICommand<Guid>;