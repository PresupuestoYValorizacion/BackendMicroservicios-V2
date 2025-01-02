using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Sgo.Paginations;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Infrastructure.Service;

namespace MsAcceso.Infrastructure.RepositoriesTenant;

internal sealed class ClienteTenantRepository : RepositoryTenant<ClienteTenant, ClienteTenantId>, IClienteTenantRepository, IPaginationClientesRepository
{
    public ClienteTenantRepository(IDbContextFactory dbContextFactory, ICurrentTenantService currentTenantService)
        : base(dbContextFactory, currentTenantService)
    {
    }

    public async Task<List<ClienteTenant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<ClienteTenant>().Where(x => x.Activo == new Activo(true) && x.TipoClienteId == TipoCliente.Cliente).ToListAsync(cancellationToken);

    }

    public async Task<ClienteTenant?> GetByNumeroDocumento(string numeroDocumento, CancellationToken cancellationToken = default)
    {

        return await DbContext.Set<ClienteTenant>().Where(x => x.Activo == new Activo(true) && x.NumeroDocumento == numeroDocumento).FirstOrDefaultAsync(cancellationToken);
    }


}