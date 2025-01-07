using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetByIdProyecto;

public sealed record GetByIdProyectoQuery : IQuery<ProyectoTenantDto>
{
    public string Id { get; set; } = string.Empty;
}