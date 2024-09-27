using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Domain.Root.UsuarioLicencias;

namespace MsAcceso.Application.Root.Users.UpdateUser;

internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    private readonly IUnitOfWorkTenant _unitOfWork;
    private readonly ITenantProvider _tenantProvider;
    private readonly IRolRepository _rolRepository;
    private readonly IUsuarioLicenciaRepository _usuarioLicenciaRepository;


    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        ITenantProvider tenantProvider,
        IRolRepository rolRepository,
        IUsuarioLicenciaRepository usuarioLicenciaRepository,
        IUnitOfWorkTenant unitOfWork)
    {
        _userRepository = userRepository;
        _tenantProvider = tenantProvider;
        _rolRepository = rolRepository;
        _usuarioLicenciaRepository = usuarioLicenciaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByIdUserIncludes(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var email = request.Email != user.Email! && request.Email is not null
            ? request.Email
            : string.Empty;

        if (!string.IsNullOrEmpty(email))
        {
            var emailExists = await _userRepository.IsUserExists(email, cancellationToken);
            if (emailExists)
            {
                return Result.Failure<Guid>(UserErrors.EmailExists);
            }
        }

        var username = request.Username != user.Username! && request.Username is not null
            ? request.Username
            : string.Empty;

        bool isAdmin = user.UsuarioLicencias!.Count == 0;

        if (request.IsAdmin != isAdmin)
        {
            await HandleAdminChangeAsync(request, user, cancellationToken);
        }
        else
        {
            await HandleUpdateAsync(request, user, cancellationToken);
        }

        user.Update(
            username,
            email,
            user.ConnectionString!,
            user.RolId!
        );

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id!.Value, Message.Update);

    }

    private async Task HandleAdminChangeAsync(UpdateUserCommand request, User user, CancellationToken cancellationToken)
    {
        if (request.IsAdmin)
        {
            await DeactivateLicensesAsync(user.Id!, cancellationToken);
            user.Update(request.Username!, request.Email!, string.Empty, request.RolId!);

            await _tenantProvider.Delete(user.Id!.Value);
        }
        else
        {
            var connectionString = await _tenantProvider.Create(user.Id!.Value, request.LicenciaId!);
            var rol = await _rolRepository.GetByLicenciaAsync(request.LicenciaId!, cancellationToken);
            user.Update(request.Username!, request.Email!, connectionString, rol?.Id!);

            var licenciaUser = UsuarioLicencia.Create(request.LicenciaId, user.Id,request.PeriodoLicenciaId);

            _usuarioLicenciaRepository.Add(licenciaUser);
        }
    }

    private async Task HandleUpdateAsync(UpdateUserCommand request, User user, CancellationToken cancellationToken)
    {
        if (request.IsAdmin)
        {
            if (request.RolId != user.RolId)
            {
                user.Update(request.Username!, request.Email!, string.Empty, request.RolId!);
            }
        }
        else
        {
            await DeactivateLicensesAsync(user.Id!, cancellationToken);
            var rol = await _rolRepository.GetByLicenciaAsync(request.LicenciaId!, cancellationToken);
            user.Update(request.Username!, request.Email!, user.ConnectionString!, rol?.Id!);

            var licenciaUser = UsuarioLicencia.Create(request.LicenciaId, user.Id!,request.PeriodoLicenciaId);

            _usuarioLicenciaRepository.Add(licenciaUser);
        }
    }

    private async Task DeactivateLicensesAsync(UserId userId, CancellationToken cancellationToken)
    {
        var usuarioLicencia = await _usuarioLicenciaRepository.GetByUserAsync(userId, cancellationToken);
        usuarioLicencia!.Desactive();
        _usuarioLicenciaRepository.Update(usuarioLicencia!);
    }
}