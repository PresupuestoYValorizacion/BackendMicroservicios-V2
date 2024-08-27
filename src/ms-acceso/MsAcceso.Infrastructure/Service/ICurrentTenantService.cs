

using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Infrastructure.Service
{
    public interface ICurrentTenantService
    {
        string? ConnectionString { get; set; }
        Guid? TenantId { get; set; }
        LicenciaId? LicenciaId { get; set; }
        public Task<bool> SetTenant(Guid tenant, Guid rolId);
    }
}
