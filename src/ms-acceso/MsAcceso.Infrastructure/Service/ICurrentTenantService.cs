

namespace MsAcceso.Infrastructure.Service
{
    public interface ICurrentTenantService
    {
        string? ConnectionString { get; set; }
        Guid? TenantId { get; set; }
        public Task<bool> SetTenant(Guid tenant);
    }
}
