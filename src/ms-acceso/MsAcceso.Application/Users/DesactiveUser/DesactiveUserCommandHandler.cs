

using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.DesactiveUser;

internal class DesactiveUserCommandHandler : ICommandHandler<DesactiveUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWorkTenant _unitOfWork;

    public DesactiveUserCommandHandler(IUserRepository userRepository, IUnitOfWorkTenant unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DesactiveUserCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdAsync(request.Id,cancellationToken);

        if(user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        user.Desactive();
        
        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value,Message.Desactivate);
         
    }
}