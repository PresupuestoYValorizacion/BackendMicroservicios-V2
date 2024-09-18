using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Ciudadanos;

namespace MsAcceso.Application.Ciudadanos.GetAllCiudadanos;

public sealed record GetAllCiudadanosQuery : IQuery<List<CiudadanoDto>>
{

}