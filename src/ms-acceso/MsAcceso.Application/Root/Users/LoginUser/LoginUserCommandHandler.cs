using System.Threading;
using System.Threading.Tasks;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Users;
using AutoMapper;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.UsuarioLicencias;

namespace MsAcceso.Application.Root.Users.LoginUser;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginUserResponse?>
{   

    private readonly IUserRepository _userRepository;
    private readonly IUsuarioLicenciaRepository _usuarioLicenciaRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    private readonly IMapper _mapper;
    
    public LoginCommandHandler(
        IUserRepository userRepository,
        IUsuarioLicenciaRepository usuarioLicenciaRepository,
        IParametroRepository parametroRepository,
        IUnitOfWorkTenant unitOfWork,
        IJwtProvider jwtProvider,
        IMapper mapper
    )
    {
        _userRepository = userRepository;
        _usuarioLicenciaRepository = usuarioLicenciaRepository;
        _parametroRepository = parametroRepository;
        _unitOfWork = unitOfWork;
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

        if(user.Rol!.TipoRolId == new ParametroId(TipoRol.Licencia))
        {
            var usuarioLicencia = await _usuarioLicenciaRepository.GetByUserAsync(user.Id!, cancellationToken);

            if(usuarioLicencia!.FechaInicio is null && usuarioLicencia.FechaFin is null)
            {
                var fechaActual = DateTime.Now;

                var periodoLicencia = await _parametroRepository.GetByIdAsync(usuarioLicencia.PeriodoLicenciaId!,cancellationToken);

                var fechaFinal = fechaActual.AddMonths(int.Parse(periodoLicencia!.Valor!));

                usuarioLicencia.Update(fechaActual,fechaFinal);

                _usuarioLicenciaRepository.Update(usuarioLicencia);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
        var token = await _jwtProvider.Generate(user);

        return LoginUserResponse.Create(token, userDto);
    }
}