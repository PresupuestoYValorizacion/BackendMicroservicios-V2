using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Opciones.DeleteOpciones;

internal class DeleteOpcionCommandHandler : ICommandHandler<DeleteOpcionCommand, Guid>
{

    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public DeleteOpcionCommandHandler(
        IOpcionRepository opcionRepository,
        IUnitOfWorkApplication unitOfWork
    )
    {
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteOpcionCommand request, CancellationToken cancellationToken)
    {

        try
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
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(OpcionErrors.OpcionInUse);

        }
    }
}