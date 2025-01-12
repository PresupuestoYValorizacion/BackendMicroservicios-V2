using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetByIdTitulo;

public sealed record GetByIdTituloQuery : IQuery<TituloTenantDto>
{
    public string Id { get; set; } = string.Empty;
}