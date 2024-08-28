using Microsoft.EntityFrameworkCore;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Rols;
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
        public async Task<bool> SetTenant(Guid tenant, Guid rolId)
        {

            var tenantInfo = await _context.Users.Where(x => x.Id == new UserId(tenant)).FirstOrDefaultAsync(); // check if tenant exists
            
            var rol = await _context.Rols.Where(x => x.Id == new RolId(rolId)).FirstOrDefaultAsync();


            if (tenantInfo != null && rol != null)
            {
                TenantId = tenant;
                ConnectionString = tenantInfo.ConnectionString; 
                LicenciaId = rol!.LicenciaId;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid"); 
            }

        }

    }
}
