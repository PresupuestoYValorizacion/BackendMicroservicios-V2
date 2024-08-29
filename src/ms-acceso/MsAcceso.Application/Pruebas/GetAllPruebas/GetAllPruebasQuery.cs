using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.GetAllPruebas;

public sealed record GetAllPruebasQuery : IQuery<List<LicenciaDto>>
{

}