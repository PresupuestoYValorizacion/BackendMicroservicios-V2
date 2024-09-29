using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.DeleteRoleTenant;

internal sealed class DeleteRoleTenantCommandHandler : ICommandHandler<DeleteRoleTenantCommand, Guid>
{
    private readonly IRolTenantRepository _rolRepository;

    public DeleteRoleTenantCommandHandler(
        IRolTenantRepository rolRepository
    )
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteRoleTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var rol = await _rolRepository.GetByIdAsync(request.RolId, cancellationToken);

            if (rol is null)
            {
                return Result.Failure<Guid>(RolErrors.RolNotExists);
            }

            _rolRepository.Delete(rol);

            await _rolRepository.SaveChangesAsync(cancellationToken);
            return Result.Success(rol.Id!.Value, Message.Delete);
        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(RolErrors.RolInUse);

        }
    }

   


}