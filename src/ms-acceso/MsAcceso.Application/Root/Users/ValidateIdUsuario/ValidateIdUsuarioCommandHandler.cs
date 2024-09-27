using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using AutoMapper;
using MsAcceso.Application.Root.Users.SingInByToken;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.ValidateIdUsuario;

internal sealed class ValidateIdUsuarioCommandHandler : ICommandHandler<ValidateIdUsuarioCommand, bool>
{

    private readonly IUserRepository _userRepository;
    
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;

    public ValidateIdUsuarioCommandHandler(IUserRepository userRepository, IMapper mapper, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<bool>> Handle(ValidateIdUsuarioCommand request, CancellationToken cancellationToken)
    {

        var exists = await _userRepository.ValidateIdUsuarioExists(request.IdUsuario, cancellationToken);

        if (!exists)
        {
            return Result.Failure<bool>(UserErrors.NotFound)!;
        }

        return exists;
    }
}