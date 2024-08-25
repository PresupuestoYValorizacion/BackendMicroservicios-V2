using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.RegisterMenuOpcion;

public sealed record RegisterMenuOpcionCommand(
    OpcionId opcionId,
    SistemaId sistemaId
) : ICommand<Guid>;