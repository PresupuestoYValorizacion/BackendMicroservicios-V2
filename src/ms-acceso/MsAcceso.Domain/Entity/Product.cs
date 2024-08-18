
using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Entity
{
    // sample business entity
    public class Product : Entity<ProductId>, IMustHaveTenant 
    {
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string TenantId { get; set; } = "";
    }
}
