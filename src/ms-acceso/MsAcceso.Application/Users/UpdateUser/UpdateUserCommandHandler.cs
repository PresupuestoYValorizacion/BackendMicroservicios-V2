

using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.UpdateUser;

internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWorkTenant _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWorkTenant unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdAsync(request.Id,cancellationToken);

        if(user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var email = "";

        if(request.Email != user.Email! && request.Email is not null) 
        {
            var emailExiste = await _userRepository.IsUserExists(request.Email!, cancellationToken);

            if(emailExiste)
            {
                return Result.Failure<Guid>(UserErrors.EmailExists);
            }

            email = request.Email;
        }

        var username = "";

        if(request.Username != user.Username! && request.Username is not null)   
        {
            username = request.Username;
        }


        user.Update(username, email);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value, Message.Update);
         
    }
}