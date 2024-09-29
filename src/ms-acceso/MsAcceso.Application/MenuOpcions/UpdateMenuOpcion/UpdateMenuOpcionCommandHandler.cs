using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.MenuOpcions.UpdateMenuOpcion;

internal sealed class UpdateMenuOpcionCommandHandler : ICommandHandler<UpdateMenuOpcionCommand, Guid>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public UpdateMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IOpcionRepository opcionRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(UpdateMenuOpcionCommand request, CancellationToken cancellationToken)
    {

        // var menuId = request.MenuOpcionId;

        var menuOpcion = await _menuOpcionRepository.GetByIdAsync(request.MenuOpcionId,cancellationToken);

        if (menuOpcion == null){
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionNotFound);
        }

        if(menuOpcion.OpcionesId != request.OpcionId)
        {
            var menuOpcionExists = await _menuOpcionRepository.MenuOpcionExists(request.OpcionId, menuOpcion.MenusId!,cancellationToken);

            if(menuOpcionExists)
            {
                return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionExists);            
            }

            var opcionExists = await _opcionRepository.GetByIdAsync(request.OpcionId,cancellationToken);

            if(opcionExists is null)
            {
                return Result.Failure<Guid>(OpcionErrors.NotFound);
            }

        }

        if(request.EsIntercambio)
        {
            var menuOpcionIntercambio = await _menuOpcionRepository.GetByOrdenAsync(request.Orden, menuOpcion.MenusId!, cancellationToken);

            if (menuOpcionIntercambio is null)
            {
                return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionIntercambioNotFound);
            }

            menuOpcionIntercambio.UpdateOrden(menuOpcion.Orden);

            _menuOpcionRepository.Update(menuOpcionIntercambio);
        }
        menuOpcion!.Update(
            request.OpcionId, 
            request.TieneUrl,
            request.Url,
            request.Orden
        );

        _menuOpcionRepository.Update(menuOpcion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(menuOpcion.Id!.Value, Message.Update);
    }
}