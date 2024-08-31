using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.DeleteMenuOpcion;

public sealed record DeleteMenuOpcionCommand(SistemaId MenuId, OpcionId OpcionId): ICommand<Guid>;