using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.Opciones.DesactiveOpciones;

public sealed record DesactiveOpcionesCommand(OpcionId Id) : ICommand<Guid>;