using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Users;


namespace MsAcceso.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonaRepository _personaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;
    private readonly ITenantProvider _tenantProvider;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWorkTenant unitOfWork, IPersonaRepository personaRepository, ITenantProvider tenantProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _personaRepository = personaRepository;
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

        var empresaId = new Guid(request.EmpresaId);

        var empresaExist = await _personaRepository.IsEmpresaExists(new PersonaId(empresaId),cancellationToken);

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
            new PersonaId(empresaId)
        );
        
        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value, Message.Create);
         
    }
}