using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.UpdateRoleTenant;

internal sealed class UpdateRoleTenantCommandHandler : ICommandHandler<UpdateRoleTenantCommand, Guid>
{

    private readonly IRolTenantRepository _rolRepository;

    public UpdateRoleTenantCommandHandler(
        IRolTenantRepository rolRepository
    )
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateRoleTenantCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByIdAsync(request.RolId,cancellationToken);

        if(rol is null)
        {
            return Result.Failure<Guid>(RolTenantErrors.RolNotExists);
        }

    

        if(rol.Nombre != request.Nombre)
        {
            var nombreExists= await _rolRepository.GetByNombreAsync(request.Nombre,cancellationToken);

            if(nombreExists)
            {
                return Result.Failure<Guid>(RolErrors.AlreadyExists);
            }

        }

        rol.Update(request.Nombre);

        _rolRepository.Update(rol);

        await _rolRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(rol.Id!.Value, Message.Update);
    }
}