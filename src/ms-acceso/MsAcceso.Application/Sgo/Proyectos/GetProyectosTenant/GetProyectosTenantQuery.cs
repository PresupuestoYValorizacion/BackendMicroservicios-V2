using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetProyectosTenant;

public sealed record GetProyectosTenantQuery : IQuery<List<ProyectoTenantDto>>
{
    
}