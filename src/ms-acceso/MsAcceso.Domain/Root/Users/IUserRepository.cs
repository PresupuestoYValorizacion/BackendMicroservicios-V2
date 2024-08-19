using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Users;

public interface IUserRepository
{

    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);

    void Add(User user);

    void Update(User user);
    
    void Delete(User user);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> IsUserExists(
        string email, 
        CancellationToken cancellationToken = default
    );
    
    Task<bool> ValidateIdUsuarioExists(
        Guid idUsuario, 
        CancellationToken cancellationToken = default
    );
    

}