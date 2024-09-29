using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Parametros.DeleteParametros;

internal sealed class DeleteParametrosCommandHandler : ICommandHandler<DeleteParametrosCommand, int>
{
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public DeleteParametrosCommandHandler(
        IParametroRepository parametroRepository,
        IUnitOfWorkApplication unitOfWork
    )
    {
        _parametroRepository = parametroRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(DeleteParametrosCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var parametroDelete = await _parametroRepository.GetByIdAsync(request.Id, cancellationToken);

            if (parametroDelete is null)
            {
                return Result.Failure<int>(ParametroErrors.ParametroNotFound);
            }

            var entities = await _parametroRepository.GetAllParametrosBySubnivelToDelete(request.Id, cancellationToken);

            entities.Add(parametroDelete);

            if (entities.Count > 0)
            {

                foreach (var relatedEntity in entities)
                {
                    _parametroRepository.Delete(relatedEntity);
                }
            }


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(1, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<int>(ParametroErrors.ParametroInUse);

        }

    }
}