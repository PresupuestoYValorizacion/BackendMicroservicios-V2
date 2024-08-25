

using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.DesactiveUser;

internal class DesactiveUserCommandHandler : ICommandHandler<DesactiveUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    private readonly IPersonaRepository _personaRepository;

    private readonly IUnitOfWorkTenant _unitOfWork;

    public DesactiveUserCommandHandler(
        IUserRepository userRepository, 
        IPersonaRepository personaRepository,
        IUnitOfWorkTenant unitOfWork)
    {
        _userRepository = userRepository;
        _personaRepository = personaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DesactiveUserCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdUserIncludes(request.Id,cancellationToken);

        if(user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var persona = await _personaRepository.GetByIdAsync(user.EmpresaId!);

        persona!.Desactive();

        user.Desactive();

        _personaRepository.Update(persona);
        
        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value,Message.Desactivate);
         
    }
}