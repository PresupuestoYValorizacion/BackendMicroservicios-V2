using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetAllRecursosTenant;

public sealed record GetAllRecursosTenantQuery : IQuery<List<RecursoTenantDto>>
{
    public int UnidadMedidaId { get; set; }
}