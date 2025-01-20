using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetByIdPartidaRecurso;

public sealed record GetByIdPartidaRecursoQuery : IQuery<PartidaRecursoTenantDto>
{
    public string Id { get; set; } = string.Empty;
}