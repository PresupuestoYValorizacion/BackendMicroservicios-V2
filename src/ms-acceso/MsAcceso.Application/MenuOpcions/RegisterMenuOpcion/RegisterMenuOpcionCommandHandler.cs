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
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public RegisterMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        ISistemaRepository sistemaRepository,
        IOpcionRepository opcionRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _sistemaRepository = sistemaRepository;
        _opcionRepository = opcionRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(RegisterMenuOpcionCommand request, CancellationToken cancellationToken)
    {
        var sistemaExists = await _sistemaRepository.SistemaGetByIdAsync(request.sistemaId,cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var opcionExists = await _opcionRepository.GetByIdAsync(request.opcionId,cancellationToken);

        if(opcionExists is null)
        {
            return Result.Failure<Guid>(OpcionErrors.NotFound);
        }

        var menuOpcionExists = await _menuOpcionRepository.MenuOpcionExists(request.opcionId,request.sistemaId,cancellationToken);

        if(menuOpcionExists)
        {
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionExists);
        }

        var menuOpcion = MenuOpcion.Create(
            request.opcionId,
            request.sistemaId
        );

        _menuOpcionRepository.Add(menuOpcion);
        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(menuOpcion.Id!.Value, Message.Create);
    }
}