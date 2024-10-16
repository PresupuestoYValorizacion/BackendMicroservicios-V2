using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using AutoMapper;
using MsAcceso.Application.Root.Users.SingInByToken;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.Sesiones;

namespace MsAcceso.Application.Root.Users.LoginUser;

internal sealed class SingInByTokenCommandHandler : ICommandHandler<SingInByTokenCommand, LoginUserResponse?>
{

    private readonly IUserRepository _userRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWorkApplication _unitOfWork;

    private readonly IMapper _mapper;


    public SingInByTokenCommandHandler(IUserRepository userRepository, IUnitOfWorkApplication unitOfWork, ISesionRepository sesionRepository, IMapper mapper, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginUserResponse?>> Handle(SingInByTokenCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null) return Result.Failure<LoginUserResponse?>(UserErrors.NotFound);

        var userDto = _mapper.Map<UserDto>(user);

        var token = request.Token;

        var expirationTime = _jwtProvider.GetExpirationTime(token);
        var timeRemaining = expirationTime - DateTime.UtcNow;

        if (timeRemaining <= TimeSpan.FromMinutes(5))
        {
            token = await _jwtProvider.Generate(user!);
            var sessionByToken = await _sesionRepository.GetByUserId(user!.Id!.Value.ToString(), cancellationToken);
            
            if (sessionByToken == null) return Result.Failure<LoginUserResponse?>(UserErrors.SessionExistente);

            sessionByToken!.Desactive();
            _sesionRepository.Update(sessionByToken);

            var sesion = Sesion.Create(userDto.Id!, token);

            _sesionRepository.Add(sesion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return LoginUserResponse.Create(token, userDto, false);
    }
}