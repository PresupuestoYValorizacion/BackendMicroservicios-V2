
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Authentication;
using MsAcceso.Domain.Abstractions;
using AutoMapper;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.Sesiones;
using MsAcceso.Application.Tenant.Users.LoginTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Tenant.Users.SingInByTokenTenant;

internal sealed class SingInByTokenTenantCommandHandler : ICommandHandler<SingInByTokenTenantCommand, LoginTenantResponse?>
{

    private readonly IUserTenantRepository _userRepository;
    private readonly ISesionRepository _sesionRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IUnitOfWorkApplication _unitOfWork;


    public SingInByTokenTenantCommandHandler(
        IUserTenantRepository userTenantRepository,
        ISesionRepository sesionRepository, 
        IMapper mapper, 
        IJwtProvider jwtProvider,
        IUnitOfWorkApplication unitOfWork

    )
    {
        _userRepository = userTenantRepository;
        _sesionRepository = sesionRepository;
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LoginTenantResponse?>> Handle(SingInByTokenTenantCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null) return Result.Failure<LoginTenantResponse?>(UserErrors.NotFound);

        var userDto = _mapper.Map<UserTenantDto>(user);

        var token = request.Token;

        var expirationTime = _jwtProvider.GetExpirationTime(token);
        var timeRemaining = expirationTime - DateTime.UtcNow;

        if (timeRemaining <= TimeSpan.FromMinutes(5))
        {
            token = await _jwtProvider.GenerateForTenant(user!, new RolId(Guid.Parse(request.RolId)) , request.TenantId);
            var sessionByToken = await _sesionRepository.GetByUserId(user!.Id!.Value.ToString(), cancellationToken);
            
            if (sessionByToken == null) return Result.Failure<LoginTenantResponse?>(UserErrors.SessionExistente);

            sessionByToken!.Desactive();
            _sesionRepository.Update(sessionByToken);

            var sesion = Sesion.Create(userDto.Id!, token);

            _sesionRepository.Add(sesion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return LoginTenantResponse.Create(token, userDto, false);
    }
}