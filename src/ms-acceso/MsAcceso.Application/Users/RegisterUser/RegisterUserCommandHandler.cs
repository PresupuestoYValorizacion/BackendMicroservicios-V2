using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;
using MsAcceso.Domain.Root.Users;


namespace MsAcceso.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonaRepository _personaRepository;
    private readonly IPersonaNaturalRepository _personaNaturalRepository;
    private readonly IPersonaJuridicaRepository _personaJuridicaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;
    private readonly ITenantProvider _tenantProvider;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWorkTenant unitOfWork, IPersonaRepository personaRepository,  IPersonaNaturalRepository personaNaturalRepository, IPersonaJuridicaRepository personaJuridicaRepository,ITenantProvider tenantProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _personaRepository = personaRepository;
        _personaJuridicaRepository = personaJuridicaRepository;
        _personaNaturalRepository = personaNaturalRepository;
        _tenantProvider = tenantProvider;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var email = request.Email;
        
        var userExists = await _userRepository.IsUserExists(email,cancellationToken);

        if (userExists)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        var personaExists = await _personaRepository.NumeroDocumentoExists(request.NumeroDocumento);

        if (personaExists){
            return Result.Failure<Guid>(PersonaErrors.AlreadyExists);
        }

        var persona = Persona.Create(PersonaId.New(), request.TipoId,request.TipoDocumentoId,request.NumeroDocumento);

        _personaRepository.Add(persona);

        await _unitOfWork.SaveChangesAsync(cancellationToken);


        if(request.TipoId == new ParametroId(TipoPersona.Natural)){

            var personaNatural = PersonaNatural.Create(persona.Id!, request.NombreCompleto);

            _personaNaturalRepository.Add(personaNatural);

            await _unitOfWork.SaveChangesAsync();

        }else if(request.TipoId == new ParametroId(TipoPersona.Juridico) ){
            
            var personaJuridica = PersonaJuridica.Create(persona.Id!, request.RazonSocial);

            _personaJuridicaRepository.Add(personaJuridica);

            await _unitOfWork.SaveChangesAsync();
        }




        var empresaId = persona.Id;

        var empresaExist = await _personaRepository.IsEmpresaExists(empresaId!,cancellationToken);

        if (!empresaExist)
        {
            return Result.Failure<Guid>(UserErrors.EmpresaNotExists);
        }
        


        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);


        var userId = UserId.New();


        var connectionString= await _tenantProvider.Create(true, userId.Value);

        var user = User.Create(
            userId,
            request.Username,
            email,
            passwordHash,
            connectionString,
            empresaId!
        );
        
        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value, Message.Create);
         
    }
}