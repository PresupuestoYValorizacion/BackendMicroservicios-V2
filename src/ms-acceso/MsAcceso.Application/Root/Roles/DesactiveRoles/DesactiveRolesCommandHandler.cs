using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Root.Roles.DesactiveRoles;

internal sealed class DesactiveRolesCommandHandler : ICommandHandler<DesactiveRolesCommand, Guid>
{
    private readonly IRolRepository _rolRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public DesactiveRolesCommandHandler(
        IRolRepository rolRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _rolRepository = rolRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(DesactiveRolesCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByIdAsync(request.RolId,cancellationToken);

        if(rol is null)
        {
            return Result.Failure<Guid>(RolErrors.RolNotExists);
        }

        rol.Desactive();

        _rolRepository.Update(rol);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(rol.Id!.Value, Message.Desactivate);
    }
}