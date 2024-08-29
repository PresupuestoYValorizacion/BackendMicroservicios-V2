using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.GetAllPruebas;

public sealed record GetPruebaByNombreQuery : IQuery<LicenciaDto>
{
    public string? Nombre {get; set;}
}