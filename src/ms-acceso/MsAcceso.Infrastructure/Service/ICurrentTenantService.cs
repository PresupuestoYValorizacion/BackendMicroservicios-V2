

using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Infrastructure.Service
{
    public interface ICurrentTenantService
    {
        string? ConnectionString { get; set; }
        Guid? TenantId { get; set; }
        RolId? RolId { get; set; }
        public Task<bool> SetTenant(Guid tenant, Guid rolId);
    }
}
