namespace MsAcceso.Application.Users.LoginUser;

using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;
using AutoMapper;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginUserResponse?>
{   

    private readonly IUserRepository _userRepository;

    private readonly IJwtProvider _jwtProvider;

    private readonly IMapper _mapper;
    
    public LoginCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<Result<LoginUserResponse?>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        var user =  await _userRepository.GetByEmailAsync(request.Email,cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.NotFound)!;
        }

        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.Password!))
        {
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials)!;
        }

        var userDto = _mapper.Map<UserDto>(user);

        var token = await _jwtProvider.Generate(user);

        return LoginUserResponse.Create(token, userDto);
    }
}