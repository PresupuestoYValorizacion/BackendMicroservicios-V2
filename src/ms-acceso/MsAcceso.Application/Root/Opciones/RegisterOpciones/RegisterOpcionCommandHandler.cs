using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.Opciones.RegisterOpciones;

internal class RegisterOpcionCommandHandler : ICommandHandler<RegisterOpcionCommand , Guid>
{

    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public RegisterOpcionCommandHandler(
        IOpcionRepository opcionRepository,
        IUnitOfWorkApplication unitOfWork
    )
    {
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterOpcionCommand request, CancellationToken cancellationToken)
    {
        var opcionExists = await _opcionRepository.OpcionExist(request.Nombre,cancellationToken);

        if(opcionExists){
            return Result.Failure<Guid>(OpcionErrors.OpcionExists);
        }
        
        var opcion = Opcion.Create(
            request.Nombre,
            request.Icono,
            request.Tooltip
        );

        _opcionRepository.Add(opcion);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(opcion.Id!.Value, Message.Create);
    }
}