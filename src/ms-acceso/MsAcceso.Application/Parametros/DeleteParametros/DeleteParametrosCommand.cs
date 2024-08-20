using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.DeleteParametros;

public sealed record DeleteParametrosCommand(
    ParametroId Id
): ICommand<int>;