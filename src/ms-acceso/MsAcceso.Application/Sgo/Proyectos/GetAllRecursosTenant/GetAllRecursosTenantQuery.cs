using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetAllRecursosTenant;

public sealed record GetAllRecursosTenantQuery : IQuery<List<RecursoTenantDto>>
{
    public int UnidadMedidaId { get; set; }
}