namespace MsAcceso.Application.Abstractions.Tenant;

public interface ITenantProvider
{
    Task<string> Create(Guid id);
    Task<bool> Delete(Guid id);

}