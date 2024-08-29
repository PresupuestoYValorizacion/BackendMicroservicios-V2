using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.GetAllActive;

public sealed record GetAllActiveQuery : IQuery<List<LicenciaDto>>
{
    
}