using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.GetSistemasByDependencia;

public sealed record GetSistemasByDependenciaQuery : IQuery<List<SistemasDto>?>
{
    public string? Dependencia {get; set;}
}