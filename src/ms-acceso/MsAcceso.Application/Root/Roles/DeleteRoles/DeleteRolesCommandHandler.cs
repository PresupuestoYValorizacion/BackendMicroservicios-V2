using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Roles.DeleteRoles;

internal sealed class DeleteRolesCommandHandler : ICommandHandler<DeleteRolesCommand, Guid>
{
    private readonly IRolRepository _rolRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public DeleteRolesCommandHandler(
        IRolRepository rolRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _rolRepository = rolRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(DeleteRolesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var rol = await _rolRepository.GetByIdAsync(request.RolId, cancellationToken);

            if (rol is null)
            {
                return Result.Failure<Guid>(RolErrors.RolNotExists);
            }

            _rolRepository.Delete(rol);

            await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);
            return Result.Success(rol.Id!.Value, Message.Delete);
        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(RolErrors.RolInUse);

        }
    }

   


}