
using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Entity
{
    public class TenantEntity : Entity<TenantId>
    {
        public string? Name { get; set; }
        public string? ConnectionString { get; set; }

    }
}
