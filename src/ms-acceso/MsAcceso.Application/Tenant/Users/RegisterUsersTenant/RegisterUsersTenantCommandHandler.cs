using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PersonasJuridicasTenant;
using MsAcceso.Domain.Tenant.PersonasNaturalesTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;


namespace MsAcceso.Application.Tenant.Users.RegisterUsersTenant;

internal class RegisterUsersTenantCommandHandler : ICommandHandler<RegisterUsersTenantCommand, Guid>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IPersonaTenantRepository _personaTenantRepository;
    private readonly IPersonaNaturalTenantRepository _personaNaturalTenantRepository;
    private readonly IPersonaJuridicaTenantRepository _personaJuridicaTenantRepository;
    private readonly IRolTenantRepository _rolTenantRepository;
    // private readonly IUnitOfWorkApplication _unitOfWork;
    // private readonly ITenantProvider _tenantProvider;

    public RegisterUsersTenantCommandHandler(
        IUserTenantRepository userTenantRepository,
        // IUnitOfWorkApplication unitOfWork,
        IPersonaTenantRepository personaTenantRepository,
        IPersonaNaturalTenantRepository personaNaturalTeantRepository,
        IPersonaJuridicaTenantRepository personaJuridicaTenantRepository,
        IRolTenantRepository rolTenantRepository
        // ITenantProvider tenantProvider
    )
    {
        _userTenantRepository = userTenantRepository;
        // _unitOfWork = unitOfWork;
        _personaTenantRepository = personaTenantRepository;
        _personaJuridicaTenantRepository = personaJuridicaTenantRepository;
        _personaNaturalTenantRepository = personaNaturalTeantRepository;
        _rolTenantRepository = rolTenantRepository;
        // _tenantProvider = tenantProvider;
    }

    public async Task<Result<Guid>> Handle(RegisterUsersTenantCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await ValidateUserAndPersonaAsync(request, cancellationToken);
        if (validationResult.IsFailure)
            return validationResult;

        var persona = CreateAndSavePersona(request);

        var user = await CreateAndSaveUserAsync(request, persona.Id!, cancellationToken);

        await _userTenantRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value, Message.Create);
    }

    private async Task<Result<Guid>> ValidateUserAndPersonaAsync(RegisterUsersTenantCommand request, CancellationToken cancellationToken)
    {
        if (await _userTenantRepository.IsUserExists(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserTenantErrors.AlreadyExists);
        }

        if (await _personaTenantRepository.NumeroDocumentoExists(request.NumeroDocumento))
        {
            return Result.Failure<Guid>(PersonaTenantErrors.AlreadyExists);
        }

        return Result.Success(Guid.Empty, "");
    }

    private PersonaTenant CreateAndSavePersona(RegisterUsersTenantCommand request)
    {
        var persona = PersonaTenant.Create(PersonaTenantId.New(), request.TipoId, request.TipoDocumentoId, request.NumeroDocumento);
        _personaTenantRepository.Add(persona);

        if (request.TipoId == TipoPersona.Natural)
        {
            var personaNatural = PersonaNaturalTenant.Create(persona.Id!, request.NombreCompleto);
            _personaNaturalTenantRepository.Add(personaNatural);
        }
        else if (request.TipoId == TipoPersona.Juridico)
        {
            var personaJuridica = PersonaJuridicaTenant.Create(persona.Id!, request.RazonSocial);
            _personaJuridicaTenantRepository.Add(personaJuridica);
        }

        return persona;
    }

    private async Task<UserTenant> CreateAndSaveUserAsync(RegisterUsersTenantCommand request, PersonaTenantId empresaId, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var userId = UserTenantId.New();
        UserTenant user;

        // var connectionString = await _tenantProvider.Create(userId.Value, request.LicenciaId);
        var rolTenant = await _rolTenantRepository.GetByIdAsync(request.RolId);

        user = UserTenant.Create(
            userId,
            request.Username,
            request.Email,
            passwordHash,
            //connectionString,
            empresaId,
            rolTenant?.Id!
        );
/*
        UserTenantId userId,
        string username,
        string email,
        string password,
        string connectionString,
        PersonaTenantId empresaId,
        RolTenantId rolId
*/
        _userTenantRepository.Add(user);

        return user;
    }
}