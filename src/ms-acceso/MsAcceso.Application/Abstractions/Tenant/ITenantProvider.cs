namespace MsAcceso.Application.Abstractions.Tenant;

public interface ITenantProvider
{
    Task<string> Create(bool isolated, Guid id);

}