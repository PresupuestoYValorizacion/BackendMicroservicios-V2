using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.UpdateParametros;

public sealed record UpdateParametrosCommand(
    ParametroId Id,
    string? Nombre,
    string? Descripcion,
    string? Abreviatura,
    string? Valor
): ICommand<int>;