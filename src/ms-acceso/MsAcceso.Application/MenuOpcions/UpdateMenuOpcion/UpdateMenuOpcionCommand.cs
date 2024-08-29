using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.UpdateMenuOpcion;

public sealed record UpdateMenuOpcionCommand(
    SistemaId MenuOpcionId,
    OpcionId OpcionIdNuevo,
    OpcionId OpcionIdAntiguo
) : ICommand<Guid>;