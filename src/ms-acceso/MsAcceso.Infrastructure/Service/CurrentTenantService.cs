using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Infrastructure.Tenants;

namespace MsAcceso.Infrastructure.Service
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;
        public Guid? TenantId { get; set; }
        public string? ConnectionString { get; set; }


        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;

        }
        public async Task<bool> SetTenant(Guid tenant)
        {

            var tenantInfo = await _context.Users.Where(x => x.Id == new UserId(tenant)).FirstOrDefaultAsync(); // check if tenant exists
            
            if (tenantInfo != null)
            {
                TenantId = tenant;
                ConnectionString = tenantInfo.ConnectionString; // optional connection string per tenant (can be null to use default database)
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid"); 
            }

        }

    }
}
