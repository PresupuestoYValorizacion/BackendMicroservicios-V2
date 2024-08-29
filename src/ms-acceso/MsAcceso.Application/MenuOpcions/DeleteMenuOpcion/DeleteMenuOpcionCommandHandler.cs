using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;

namespace MsAcceso.Application.MenuOpcions.DeleteMenuOpcion;

internal sealed class DeleteMenuOpcionCommandHandler : ICommandHandler<DeleteMenuOpcionCommand, Guid>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public DeleteMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(DeleteMenuOpcionCommand request, CancellationToken cancellationToken)
    {
        var menuOpcion = await _menuOpcionRepository.GetMenuOpcion(request.OpcionId,request.MenuId,cancellationToken);

        if(menuOpcion is null)
        {
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionNotFound);
        }

        _menuOpcionRepository.Delete(menuOpcion);

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(menuOpcion.Id!.Value, Message.Delete);
    }
}