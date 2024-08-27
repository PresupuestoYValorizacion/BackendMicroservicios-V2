using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Infrastructure.Service
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly ApplicationDbContext _context;
        public Guid? TenantId { get; set; }
        public string? ConnectionString { get; set; }
        public LicenciaId? LicenciaId { get; set; }

        public CurrentTenantService(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<bool> SetTenant(Guid tenant, Guid licenciaId)
        {

            var tenantInfo = await _context.Users.Where(x => x.Id == new UserId(tenant)).FirstOrDefaultAsync(); // check if tenant exists
            
            var licencia = await _context.Licencias.Where(x => x.Id == new LicenciaId(licenciaId)).FirstOrDefaultAsync();

            if (tenantInfo != null && licencia != null)
            {
                TenantId = tenant;
                ConnectionString = tenantInfo.ConnectionString; 
                LicenciaId = licencia!.Id;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid"); 
            }

        }

    }
}
