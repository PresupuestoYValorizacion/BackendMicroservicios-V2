using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.RegisterRoleTenant;

internal sealed class RegisterRolesCommandHandler : ICommandHandler<RegisterRoleTenantCommand, Guid>
{

    private readonly IRolTenantRepository _rolRepository;

    public RegisterRolesCommandHandler(
        IRolTenantRepository rolRepository
    )
    {
        _rolRepository = rolRepository;
    }

    public async Task<Result<Guid>> Handle(RegisterRoleTenantCommand request, CancellationToken cancellationToken)
    {
        
        var nombreExists= await _rolRepository.GetByNombreAsync(request.Nombre,cancellationToken);

        if(nombreExists)
        {
            return Result.Failure<Guid>(RolErrors.AlreadyExists);
        }

        var newRol = RolTenant.Create(
                request.Nombre
        );

        _rolRepository.Add(newRol);

        await _rolRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newRol.Id!.Value, Message.Create);
    }
}