using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.UsuarioLicencias;


namespace MsAcceso.Application.Root.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonaRepository _personaRepository;
    private readonly IPersonaNaturalRepository _personaNaturalRepository;
    private readonly IPersonaJuridicaRepository _personaJuridicaRepository;
    private readonly IRolRepository _rolRepository;
    private readonly IUsuarioLicenciaRepository _usuarioLicenciaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;
    private readonly ITenantProvider _tenantProvider;

    public RegisterUserCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWorkTenant unitOfWork, 
        IPersonaRepository personaRepository, 
        IPersonaNaturalRepository personaNaturalRepository, 
        IPersonaJuridicaRepository personaJuridicaRepository,
         IRolRepository rolRepository, 
         IUsuarioLicenciaRepository usuarioLicenciaRepository,
         ITenantProvider tenantProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _personaRepository = personaRepository;
        _personaJuridicaRepository = personaJuridicaRepository;
        _personaNaturalRepository = personaNaturalRepository;
        _rolRepository = rolRepository;
        _usuarioLicenciaRepository = usuarioLicenciaRepository;
        _tenantProvider = tenantProvider;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await ValidateUserAndPersonaAsync(request, cancellationToken);
        if (validationResult.IsFailure)
            return validationResult;

        var persona = CreateAndSavePersona(request);

        var user = await CreateAndSaveUserAsync(request, persona.Id!, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return Result.Success(user.Id!.Value, Message.Create);
    }

    private async Task<Result<Guid>> ValidateUserAndPersonaAsync(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsUserExists(request.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        if (await _personaRepository.NumeroDocumentoExists(request.NumeroDocumento))
        {
            return Result.Failure<Guid>(PersonaErrors.AlreadyExists);
        }

        return Result.Success(Guid.Empty,"");
    }

    private Persona CreateAndSavePersona(RegisterUserCommand request)
    {
        var persona = Persona.Create(PersonaId.New(), request.TipoId, request.TipoDocumentoId, request.NumeroDocumento);
        _personaRepository.Add(persona);

        if (request.TipoId == new ParametroId(TipoPersona.Natural))
        {
            var personaNatural = PersonaNatural.Create(persona.Id!, request.NombreCompleto);
            _personaNaturalRepository.Add(personaNatural);
        }
        else if (request.TipoId == new ParametroId(TipoPersona.Juridico))
        {
            var personaJuridica = PersonaJuridica.Create(persona.Id!, request.RazonSocial);
            _personaJuridicaRepository.Add(personaJuridica);
        }

        return persona;
    }

    private async Task<User> CreateAndSaveUserAsync(RegisterUserCommand request, PersonaId empresaId, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var userId = UserId.New();
        User user;
        UsuarioLicencia licenciaUser;

        if (request.IsAdmin)
        {
            user = User.Create(
                userId,
                request.Username,
                request.Email,
                passwordHash,
                string.Empty,
                empresaId,
                request.RolId!
            );
        }
        else
        {
            var connectionString = await _tenantProvider.Create(userId.Value, request.LicenciaId);
            var rol = await _rolRepository.GetByLicenciaAsync(request.LicenciaId!, cancellationToken);


            user = User.Create(
                userId,
                request.Username,
                request.Email,
                passwordHash,
                connectionString,
                empresaId,
                rol?.Id!
            );

            licenciaUser = UsuarioLicencia.Create(request.LicenciaId, userId,request.PeriodoLicenciaId);
             
            _usuarioLicenciaRepository.Add(licenciaUser);   
        }

        _userRepository.Add(user);

        return user;
    }
}