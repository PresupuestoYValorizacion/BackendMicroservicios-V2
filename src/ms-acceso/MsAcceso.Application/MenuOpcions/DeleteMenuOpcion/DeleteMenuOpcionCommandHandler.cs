using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.MenuOpcions.DeleteMenuOpcion;

internal sealed class DeleteMenuOpcionCommandHandler : ICommandHandler<DeleteMenuOpcionCommand, Guid>
{
    private readonly IMenuOpcionRepository _menuOpcionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public DeleteMenuOpcionCommandHandler(
        IMenuOpcionRepository menuOpcionRepository,
        IUnitOfWorkApplication unitOfWorkTenant
    )
    {
        _menuOpcionRepository = menuOpcionRepository;
        _unitOfWork = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(DeleteMenuOpcionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var menuOpcion = await _menuOpcionRepository.GetByIdAsync(request.MenuOpcionId,cancellationToken);

            if(menuOpcion is null)
            {
                return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionNotFound);
            }

            _menuOpcionRepository.Delete(menuOpcion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(menuOpcion.Id!.Value, Message.Delete);

        }
         catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(MenuOpcionErrors.MenuOpcionInUse);

        }
    }
}