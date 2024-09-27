using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Root.Parametros.DeleteParametros;

public sealed record DeleteParametrosCommand(
    ParametroId Id
): ICommand<int>;