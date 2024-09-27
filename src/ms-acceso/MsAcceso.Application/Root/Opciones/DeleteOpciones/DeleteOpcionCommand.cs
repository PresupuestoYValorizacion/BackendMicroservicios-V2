using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.Opciones.DeleteOpciones;

public sealed record DeleteOpcionCommand(OpcionId OpcionId) : ICommand<Guid>;