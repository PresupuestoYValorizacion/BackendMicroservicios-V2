

using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;
using AutoMapper;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Root.Sesiones;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.LoginTenant;

internal sealed class LoginTenantCommandHandler : ICommandHandler<LoginTenantCommand, LoginTenantResponse?>
{

    private readonly IUserTenantRepository _userRepository;
    // private readonly IUsuarioLicenciaRepository _usuarioLicenciaRepository;
    // private readonly IParametroRepository _parametroRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    private readonly IMapper _mapper;

    public LoginTenantCommandHandler(
        IUserTenantRepository userRepository,
        // IUsuarioLicenciaRepository usuarioLicenciaRepository,
        // IParametroRepository parametroRepository,
        IUnitOfWorkApplication unitOfWork,
        ISesionRepository sesionRepository,
        IJwtProvider jwtProvider,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        // _usuarioLicenciaRepository = usuarioLicenciaRepository;
        // _parametroRepository = parametroRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<Result<LoginTenantResponse?>> Handle(LoginTenantCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginTenantResponse>(UserErrors.InvalidCredentials)!;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password!))
        {
            return Result.Failure<LoginTenantResponse>(UserErrors.InvalidCredentials)!;
        }

        var sessionExistente = await _sesionRepository.GetByUserId(user.Id!.Value.ToString(), cancellationToken);


        if (sessionExistente != null )
        {

            var sessionTotalMinutes = DateTime.UtcNow.Subtract(sessionExistente!.LastActivity ?? new DateTime()).TotalMinutes;

            if (sessionTotalMinutes <= 10 && !request.IsForcedSession)
            {
                return LoginTenantResponse.HasSession(true);
            }
            else
            {
                sessionExistente.Desactive();
                _sesionRepository.Update(sessionExistente);
            }

        }

        var userDto = _mapper.Map<UserTenantDto>(user);

        
        var token = await _jwtProvider.GenerateForTenant(user);

        var sesion = Sesion.Create(userDto.Id!, token);

        _sesionRepository.Add(sesion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return LoginTenantResponse.Create(token, userDto, false);
    }
}