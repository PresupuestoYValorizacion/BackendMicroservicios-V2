using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.MenuOpcions.RegisterMenuOpcion;

internal sealed class RegisterMenuOpcionCommandHandler : ICommandHandler<RegisterMenuOpcionCommand, Guid>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public RegisterMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        ISistemaRepository sistemaRepository,
        IOpcionRepository opcionRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _sistemaRepository = sistemaRepository;
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(RegisterMenuOpcionCommand request, CancellationToken cancellationToken)
    {
        if(request.OpcionId is null){
            return Result.Failure<Guid>(Error.NullValue);
        }

        var sistemaExists = await _sistemaRepository.GetByIdAsync(request.SistemaId,cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var opcionExists = await _opcionRepository.GetByIdAsync(request.OpcionId,cancellationToken);

        if(opcionExists is null)
        {
            return Result.Failure<Guid>(OpcionErrors.NotFound);
        }

        var menuOpcionExists = await _menuOpcionRepository.MenuOpcionExists(request.OpcionId,request.SistemaId,cancellationToken);

        if(menuOpcionExists)
        {
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionExists);
        }
        
        var orden = await _menuOpcionRepository.GetCountOpcionesByMenu(request.SistemaId, cancellationToken);

        orden = (orden == 0) ? 1 : (orden + 1);
        
        var menuOpcion = MenuOpcion.Create(
            request.OpcionId,
            request.SistemaId,
            request.TieneUrl,
            request.Url,
            orden
        );

        _menuOpcionRepository.Add(menuOpcion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(menuOpcion.Id!.Value, Message.Create);
    }
}