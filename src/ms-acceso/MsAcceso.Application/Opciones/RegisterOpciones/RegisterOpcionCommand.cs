using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Opciones.RegisterOpciones;

public sealed record RegisterOpcionCommand(
    string Nombre, 
    string Logo, 
    string Abreviatura): ICommand<Guid>;