using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.DeleteMenuOpcion;

public sealed record DeleteMenuOpcionCommand(MenuOpcionId MenuOpcionId): ICommand<Guid>;