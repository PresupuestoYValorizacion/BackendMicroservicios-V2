using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.DesactiveOpciones;

public sealed record DesactiveOpcionesCommand(OpcionId Id) : ICommand<Guid>;