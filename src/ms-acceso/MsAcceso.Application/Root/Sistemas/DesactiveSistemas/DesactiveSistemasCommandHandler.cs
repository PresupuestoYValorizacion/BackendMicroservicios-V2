using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Sistemas.DesactiveSistemas;

internal sealed class DesactiveSistemasCommandHandler : ICommandHandler<DesactiveSistemasCommand, Guid>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;
    private readonly IMenuOpcionRepository _menuOpcionRepository;


    public DesactiveSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkApplication unitOfWorkTenant,
        IMenuOpcionRepository menuOpcionRepository
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWork = unitOfWorkTenant;
        _menuOpcionRepository = menuOpcionRepository;
    }
    
    public async Task<Result<Guid>> Handle(DesactiveSistemasCommand request, CancellationToken cancellationToken)
    {
        var sistemaId = new SistemaId(Guid.Parse(request.Id));

        var sistemaExists = await _sistemaRepository.GetByIdAsync(sistemaId,cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var sistemas = await _sistemaRepository.GetAllSistemasBySubnivel(sistemaExists.Id!,cancellationToken);

        sistemas.Add(sistemaExists);

        foreach(var sistema in sistemas)
        {
            var menusOpciones= await _menuOpcionRepository.GetAllMenuOpcionsByMenuId(sistema.Id!,cancellationToken);
            
            if(menusOpciones.Count > 0)
            {
                foreach (var menuOpcion in menusOpciones)
                {
                    menuOpcion.Desactive();
                    _menuOpcionRepository.Update(menuOpcion);
                }
            }

            sistema.Desactive();
            _sistemaRepository.Update(sistema);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(sistemaExists.Id!.Value, Message.Desactivate);
    }
}