using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Abstractions.Tenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.UsuarioLicencias;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;

namespace MsAcceso.Application.Tenant.Users.UpdateUsersTenant;

internal class UpdateUsersTenantCommandHandler : ICommandHandler<UpdateUsersTenantCommand, Guid>
{
    private readonly IUserTenantRepository _userTenantRepository;
    // private readonly IUnitOfWorkApplication _unitOfWork;
    // private readonly ITenantProvider _tenantProvider;
    // private readonly IRolTenantRepository _rolTenantRepository;


    public UpdateUsersTenantCommandHandler(
        IUserTenantRepository userTenantRepository
    )
    {
        _userTenantRepository = userTenantRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateUsersTenantCommand request, CancellationToken cancellationToken)
    {

        var userTenant = await _userTenantRepository.GetByIdUserIncludes(request.Id, cancellationToken);

        if (userTenant is null)
        {
            return Result.Failure<Guid>(UserTenantErrors.NotFound);
        }

        var email = request.Email != userTenant.Email! && request.Email is not null
            ? request.Email
            : string.Empty;

        if (!string.IsNullOrEmpty(email))
        {
            var emailExists = await _userTenantRepository.IsUserExists(email, cancellationToken);
            if (emailExists)
            {
                return Result.Failure<Guid>(UserTenantErrors.EmailExists);
            }
        }

        var username = request.Username != userTenant.Username! && request.Username is not null
            ? request.Username
            : string.Empty;

        userTenant.Update(
            username,
            email,
            userTenant.RolId!
        );

        _userTenantRepository.Update(userTenant);

        await _userTenantRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(userTenant.Id!.Value, Message.Update);

    }
}