using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using AutoMapper;
using MsAcceso.Application.Root.Users.SingInByToken;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.ValidateIdUsuario;

internal sealed class ValidateIdUsuarioCommandHandler : ICommandHandler<ValidateIdUsuarioCommand, string>
{

    private readonly IUserRepository _userRepository;
    
    public ValidateIdUsuarioCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
       
    }

    public async Task<Result<string>> Handle(ValidateIdUsuarioCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdAsync( new UserId(request.IdUsuario), cancellationToken);

        if (user == null)
        {
            return Result.Failure<string>(UserErrors.NotFound)!;
        }


        return user.RolId!.Value.ToString()!;
    }
}