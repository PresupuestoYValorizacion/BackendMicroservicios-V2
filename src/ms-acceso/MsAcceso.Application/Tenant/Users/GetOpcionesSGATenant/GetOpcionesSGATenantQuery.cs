using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Users.GetOpcionesSGATenant;

public sealed record GetOpcionesSGATenantQuery : IQuery<List<MenuOpcionDto>>
{
    public RolId? UserRolId { get; set; }
    public RolTenantId? RolId { get; set; }
    public string? Url { get; set; }
}