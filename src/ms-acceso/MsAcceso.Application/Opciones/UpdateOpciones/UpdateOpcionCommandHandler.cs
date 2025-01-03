using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Opciones.UpdateOpciones;

internal class UpdateOpcionCommandHandler : ICommandHandler<UpdateOpcionCommand, Guid>
{

    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public UpdateOpcionCommandHandler(
        IOpcionRepository opcionRepository,
        IUnitOfWorkTenant unitOfWork)
    {
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateOpcionCommand request, CancellationToken cancellationToken)
    {
        var opcion =  await _opcionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (opcion is null)
        {
            return Result.Failure<Guid>(OpcionErrors.NotFound);
        }
        
        var opcionExists = await _opcionRepository.OpcionExist(request.Nombre,cancellationToken);

        if(opcion.Nombre != request.Nombre && opcionExists){
            return Result.Failure<Guid>(OpcionErrors.OpcionExists);
        }

        opcion.Update(request.Nombre,
                      request.Logo,
                      request.Abreviatura); 


        _opcionRepository.Update(opcion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(opcion.Id!.Value, Message.Update);
    }
} 