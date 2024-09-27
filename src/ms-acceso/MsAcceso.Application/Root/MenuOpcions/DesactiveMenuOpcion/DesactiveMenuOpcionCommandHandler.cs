using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.Root.MenuOpcions.DesactiveMenuOpcions;

internal sealed class DesactiveMenuOpcionCommandHandler : ICommandHandler<DesactiveMenuOpcionCommand, Guid>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public DesactiveMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(DesactiveMenuOpcionCommand request, CancellationToken cancellationToken)
    {

        var menuOpcion = await _menuOpcionRepository.GetByIdAsync(request.MenuOpcionId,cancellationToken);

        if(menuOpcion is null)
        {
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionNotFound);
        }

        menuOpcion.Desactive();

        _menuOpcionRepository.Update(menuOpcion);

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(menuOpcion.Id!.Value, Message.Desactivate2(menuOpcion.Activo!.Value));
    }
}