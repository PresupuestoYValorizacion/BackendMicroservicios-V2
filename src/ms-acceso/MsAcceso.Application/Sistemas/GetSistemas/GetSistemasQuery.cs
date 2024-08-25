using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Sistemas.GetSistemas;

public sealed record GetSistemasQuery : IQuery<List<SistemasDto>>
{
    
}