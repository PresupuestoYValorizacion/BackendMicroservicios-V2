using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.GetSistemasByDependencia;

public sealed record GetSistemasByDependenciaQuery : IQuery<List<SistemaDto>>
{
    public SistemaId? Dependencia { get; set; }
}