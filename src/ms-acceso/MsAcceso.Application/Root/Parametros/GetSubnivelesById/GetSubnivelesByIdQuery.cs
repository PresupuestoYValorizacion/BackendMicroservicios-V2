using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Root.Parametros.GetSubnivelesById;

public sealed record GetSubnivelesByIdQuery : IQuery<List<ParametroDto>>
{
    public int Id { get; set; }
}