using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.DesactiveParametros;

public sealed record DesactiveParametrosCommand(
    ParametroId Id
): ICommand<int>;