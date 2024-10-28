
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.GetUserByIdTenant;

public sealed record GetUserByIdTenantQuery : PaginationParams, IQuery<UserTenantDto?>
{
    public Guid Id { get; set; }

}