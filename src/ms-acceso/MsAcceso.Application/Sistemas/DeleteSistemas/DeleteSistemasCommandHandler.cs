using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.DeleteSistemas;

internal sealed class DeleteSistemasCommandHandler : ICommandHandler<DeleteSistemasCommand, Guid>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public DeleteSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkTenant unitOfWorkTenant,
        IMenuOpcionRepository menuOpcionRepository
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
        _menuOpcionRepository = menuOpcionRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteSistemasCommand request, CancellationToken cancellationToken)
    {
        var sistemaId = new SistemaId(Guid.Parse(request.Id));

        var sistemaExists = await _sistemaRepository.GetByIdAsync(sistemaId, cancellationToken);

        if (sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var sistemas = await _sistemaRepository.GetAllSistemasBySubnivel(sistemaId, cancellationToken);

        sistemas.Add(sistemaExists);

        if (sistemas.Count > 0)
        {
            foreach (var sistema in sistemas)
            {
                var menusOpciones= await _menuOpcionRepository.GetAllMenuOpcionsByMenuId(sistema.Id!,cancellationToken);
                
                if(menusOpciones.Count > 0)
                {
                    foreach (var menuOpcion in menusOpciones)
                    {
                        _menuOpcionRepository.Delete(menuOpcion);
                    }
                }
                
                _sistemaRepository.Delete(sistema);
            }
        }

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(sistemaExists.Id!.Value, Message.Delete);
    }
}