using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.DeleteOpciones;

internal class DeleteOpcionCommandHandler : ICommandHandler<DeleteOpcionCommand, Guid>
{
    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DeleteOpcionCommandHandler(
        IOpcionRepository opcionRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteOpcionCommand request, CancellationToken cancellationToken)
    {
        var opcion = await _opcionRepository.GetByIdAsync(request.OpcionId,cancellationToken);

        if(opcion is null)
        {
            return Result.Failure<Guid>(OpcionErrors.NotFound);
        }

        _opcionRepository.Delete(opcion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(opcion.Id!.Value, Message.Delete);
    }
}