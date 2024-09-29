using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Root.Licencias.GetAllLicencias;

public sealed record GetAllLicenciasQuery : IQuery<List<LicenciaDto>>
{

}