using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.DesactiveUserTenant;

internal class DesactiveUserTenantCommandHandler : ICommandHandler<DesactiveUserTenantCommand, Guid>
{
    private readonly IUserTenantRepository _userRepository;

    private readonly IPersonaTenantRepository _personaRepository;


    public DesactiveUserTenantCommandHandler(
        IUserTenantRepository userRepository, 
        IPersonaTenantRepository personaRepository
        )
    {
        _userRepository = userRepository;
        _personaRepository = personaRepository;
    }

    public async Task<Result<Guid>> Handle(DesactiveUserTenantCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdUserIncludes(request.Id,cancellationToken);

        if(user is null)
        {
            return Result.Failure<Guid>(UserTenantErrors.NotFound);
        }

        var persona = await _personaRepository.GetByIdAsync(user.PersonaId!);

        persona!.Desactive();

        user.Desactive();

        _personaRepository.Update(persona);
        
        _userRepository.Update(user);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value,Message.Desactivate);
         
    }
}