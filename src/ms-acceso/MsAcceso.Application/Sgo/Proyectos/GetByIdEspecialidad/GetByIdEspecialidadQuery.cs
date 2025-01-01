using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetByIdEspecialidad;

public sealed record GetByIdEspecialidadQuery : IQuery<EspecialidadTenantDto>
{
    public string Id { get; set; } = string.Empty;
}