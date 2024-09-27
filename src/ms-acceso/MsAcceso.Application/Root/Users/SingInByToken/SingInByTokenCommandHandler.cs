using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using AutoMapper;
using MsAcceso.Application.Root.Users.SingInByToken;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.LoginUser;

internal sealed class SingInByTokenCommandHandler : ICommandHandler<SingInByTokenCommand, LoginUserResponse?>
{   

    private readonly IUserRepository _userRepository;

    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    
    public SingInByTokenCommandHandler(IUserRepository userRepository, IMapper mapper, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginUserResponse?>> Handle(SingInByTokenCommand request, CancellationToken cancellationToken)
    {

        var user =  await _userRepository.GetByEmailAsync(request.Email,cancellationToken);

        var userDto = _mapper.Map<UserDto>(user);

        var token = await _jwtProvider.Generate(user!);

        return LoginUserResponse.Create(token, userDto);
    }
}