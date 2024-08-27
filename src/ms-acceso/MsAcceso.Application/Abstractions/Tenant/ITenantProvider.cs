using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Abstractions.Tenant;

public interface ITenantProvider
{
    Task<string> Create(Guid id, LicenciaId licenciaId);
    Task<bool> Delete(Guid id);

}