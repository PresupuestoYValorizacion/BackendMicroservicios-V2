using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Root.Parametros.GetByIdParametro;

public sealed record GetByIdParametroQuery : IQuery<ParametroDto>
{
    public int Id { get; set; }
}