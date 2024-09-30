using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Opciones.RegisterOpciones;

public sealed record RegisterOpcionCommand(
    string Nombre, 
    string Icono, 
    string Tooltip
): ICommand<Guid>;