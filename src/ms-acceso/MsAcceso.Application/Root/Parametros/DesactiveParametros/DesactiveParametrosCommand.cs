using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Root.Parametros.DesactiveParametros;

public sealed record DesactiveParametrosCommand(
    ParametroId Id
): ICommand<int>;