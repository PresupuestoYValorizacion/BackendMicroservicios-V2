using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.Opciones.UpdateOpciones;

public sealed record UpdateOpcionCommand(
    OpcionId Id,
    string Nombre, 
    string Icono, 
    string Tooltip
): ICommand<Guid>;