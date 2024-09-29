using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.DesactiveOpciones;

internal sealed class DesactiveOpcionesCommandHandler : ICommandHandler<DesactiveOpcionesCommand, Guid>
{
    private readonly IOpcionRepository _opcionRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public DesactiveOpcionesCommandHandler(
        IOpcionRepository opcionRepository,
        IUnitOfWorkApplication unitOfWork
    )
    {
        _opcionRepository = opcionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DesactiveOpcionesCommand request, CancellationToken cancellationToken)
    {
        var opcionDesactive = await _opcionRepository.GetByIdAsync(request.Id,cancellationToken);

        if(opcionDesactive is null)
        {
            return Result.Failure<Guid>(OpcionErrors.NotFound);
        }

        opcionDesactive.Desactive();

        _opcionRepository.Update(opcionDesactive);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(opcionDesactive.Id!.Value, Message.Desactivate);
    }
}