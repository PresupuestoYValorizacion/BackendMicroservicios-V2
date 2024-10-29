
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Domain.Tenant.Users;

public interface IUserTenantRepository
{

    Task<UserTenant?> GetByIdAsync(UserTenantId id, CancellationToken cancellationToken = default);

    void Add(UserTenant user);

    void Update(UserTenant user);
    
    void Delete(UserTenant user);

    Task<UserTenant?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> IsUserExists(
        string email, 
        CancellationToken cancellationToken = default
    );
    
    Task<bool> ValidateIdUsuarioExists(
        Guid idUsuario, 
        CancellationToken cancellationToken = default
    );
    
    public  Task<UserTenant?> GetByIdUserIncludes(
        UserTenantId Id,
        CancellationToken cancellationToken = default
    );
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);


}