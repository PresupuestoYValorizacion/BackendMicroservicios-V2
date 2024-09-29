using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.DesactiveRoleTenant;

internal sealed class DesactiveRolesCommandHandler : ICommandHandler<DesactiveRoleTenantCommand, Guid>
{
    private readonly IRolTenantRepository _rolRepository;

    public DesactiveRolesCommandHandler(
        IRolTenantRepository rolRepository
    )
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<Guid>> Handle(DesactiveRoleTenantCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByIdAsync(request.RolId,cancellationToken);

        if(rol is null)
        {
            return Result.Failure<Guid>(RolErrors.RolNotExists);
        }

        rol.Desactive();

        _rolRepository.Update(rol);

        await _rolRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(rol.Id!.Value, Message.Desactivate);
    }
}