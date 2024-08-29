using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.DesactiveMenuOpcions;

public sealed record DesactiveMenuOpcionCommand(
    SistemaId MenuId,
    OpcionId OpcionId   
): ICommand<Guid>;