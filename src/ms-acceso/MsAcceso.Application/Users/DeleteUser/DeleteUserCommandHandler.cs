

using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Clock;

using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.DeleteUser;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userDelete = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (userDelete is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        _userRepository.Delete(userDelete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(request.Id.Value, Message.Delete);
    }
}