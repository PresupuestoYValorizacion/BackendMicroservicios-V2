using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.DeleteParametros;

internal sealed class DeleteParametrosCommandHandler : ICommandHandler<DeleteParametrosCommand, int>
{
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DeleteParametrosCommandHandler(
        IParametroRepository parametroRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _parametroRepository = parametroRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(DeleteParametrosCommand request, CancellationToken cancellationToken)
    {
        var parametroDelete = await _parametroRepository.GetByIdAsync(request.Id, cancellationToken);

        if (parametroDelete is null)
        {
            return Result.Failure<int>(ParametroErrors.ParametroNotFound);
        }

        var relatedEntities = await _parametroRepository.GetRelatedEntitiesAsync(request.Id.Value, cancellationToken);

        if(relatedEntities.Count > 0){

            foreach (var relatedEntity in relatedEntities)
            {
                _parametroRepository.Delete(relatedEntity);
            }
        }


        _parametroRepository.Delete(parametroDelete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(1, Message.Delete);
    }
}