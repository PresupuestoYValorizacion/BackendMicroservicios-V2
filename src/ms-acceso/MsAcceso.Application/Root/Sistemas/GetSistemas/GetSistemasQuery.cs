using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Sistemas.GetSistemas;

public sealed record GetSistemasQuery : IQuery<List<SistemaDto>>
{
    
}