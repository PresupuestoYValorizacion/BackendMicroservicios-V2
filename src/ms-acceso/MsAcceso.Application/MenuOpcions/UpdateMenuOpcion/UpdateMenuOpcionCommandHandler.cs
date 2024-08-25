using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.UpdateMenuOpcion;

internal sealed class UpdateMenuOpcionCommandHandler : ICommandHandler<UpdateMenuOpcionCommand, Guid>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public UpdateMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IOpcionRepository opcionRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _opcionRepository = opcionRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(UpdateMenuOpcionCommand request, CancellationToken cancellationToken)
    {
        var menuOpcionId = request.MenuOpcionId;
        var menuOpcion = await _menuOpcionRepository.GetByIdAsync(menuOpcionId,cancellationToken);

        if(menuOpcion is null)
        {
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionNotFound);
        }

        var opcionExists = await _opcionRepository.GetByIdAsync(request.OpcionId,cancellationToken);

        if(opcionExists is null)
        {
            return Result.Failure<Guid>(OpcionErrors.NotFound);
        }

        var menuId = menuOpcion.MenuId;
        var menuOpcionExists = await _menuOpcionRepository.MenuOpcionExists(opcionExists.Id!,menuId!,cancellationToken);

        if(menuOpcionExists){
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionExists);
        }

        menuOpcion.Update(request.OpcionId);

        _menuOpcionRepository.Update(menuOpcion);
        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(menuOpcion.Id!.Value, Message.Update);
    }
}