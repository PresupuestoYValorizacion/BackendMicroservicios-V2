using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetTitulosByEspecialidad;

public sealed record GetTitulosByEspecialidadQuery : IQuery<List<PresupuestoEspecialidadTituloTenantDto>>
{
    public string EspecialidadId { get; set; } = string.Empty;
}