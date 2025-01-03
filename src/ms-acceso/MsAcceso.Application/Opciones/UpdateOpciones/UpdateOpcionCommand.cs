using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.UpdateOpciones;

public sealed record UpdateOpcionCommand(
    OpcionId Id,
    string Nombre, 
    string Logo, 
    string Abreviatura): ICommand<Guid>;