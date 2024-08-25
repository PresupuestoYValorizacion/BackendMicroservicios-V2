using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.GetSistemasById;

public sealed record GetSistemasByIdQuery : IQuery<SistemaDto?>
{
    public string? Id {get; set;}
}