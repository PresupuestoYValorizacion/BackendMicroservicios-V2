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

namespace MsAcceso.Application.Root.Users.LoginUser;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginUserResponse?>
{

    private readonly IUserRepository _userRepository;
    private readonly IUsuarioLicenciaRepository _usuarioLicenciaRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    private readonly IMapper _mapper;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IUsuarioLicenciaRepository usuarioLicenciaRepository,
        IParametroRepository parametroRepository,
        IUnitOfWorkApplication unitOfWork,
        ISesionRepository sesionRepository,
        IJwtProvider jwtProvider,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _usuarioLicenciaRepository = usuarioLicenciaRepository;
        _parametroRepository = parametroRepository;
        _sesionRepository = sesionRepository;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<Result<LoginUserResponse?>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials)!;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password!))
        {
            return Result.Failure<LoginUserResponse>(UserErrors.InvalidCredentials)!;
        }

        var sessionExistente = await _sesionRepository.GetByUserId(user.Id!.Value.ToString(), cancellationToken);



        if (sessionExistente != null )
        {

            var sessionTotalMinutes = DateTime.UtcNow.Subtract(sessionExistente!.LastActivity ?? new DateTime()).TotalMinutes;

            if (sessionTotalMinutes <= 10 && !request.IsForcedSession)
            {
                return LoginUserResponse.HasSession(true);
            }
            else
            {
                sessionExistente.Desactive();
                _sesionRepository.Update(sessionExistente);
            }

        }



        var userDto = _mapper.Map<UserDto>(user);

        if (user.Rol!.TipoRolId == new ParametroId(TipoRol.Licencia))
        {
            var usuarioLicencia = await _usuarioLicenciaRepository.GetByUserAsync(user.Id!, cancellationToken);

            if (usuarioLicencia!.FechaInicio is null && usuarioLicencia.FechaFin is null)
            {
                var fechaActual = DateTime.Now;

                var periodoLicencia = await _parametroRepository.GetByIdAsync(usuarioLicencia.PeriodoLicenciaId!, cancellationToken);

                var fechaFinal = fechaActual.AddMonths(int.Parse(periodoLicencia!.Valor!));

                usuarioLicencia.Update(fechaActual, fechaFinal);

                _usuarioLicenciaRepository.Update(usuarioLicencia);

            }
        }
        var token = await _jwtProvider.Generate(user);

        var sesion = Sesion.Create(userDto.Id!, token);

        _sesionRepository.Add(sesion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return LoginUserResponse.Create(token, userDto, false);
    }
}