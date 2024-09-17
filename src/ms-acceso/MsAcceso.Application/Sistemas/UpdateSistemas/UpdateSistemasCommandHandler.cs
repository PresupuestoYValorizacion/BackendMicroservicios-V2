using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.UpdateSistemas;

internal sealed class UpdateSistemasCommandHandler : ICommandHandler<UpdateSistemasCommand, Guid>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public UpdateSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }
    
    public async Task<Result<Guid>> Handle(UpdateSistemasCommand request, CancellationToken cancellationToken)
    {

        var sistemaExists = await _sistemaRepository.GetByIdAsync(request.Id,cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var nombreSistemaExists = await _sistemaRepository.SistemaExistsByName(request.Nombre!, cancellationToken);

        if(nombreSistemaExists && sistemaExists.Nombre != request.Nombre)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotAvailable);
        }

        var urlSistemaExists = await _sistemaRepository.SistemaExistsByUrl(request.Url!, cancellationToken);

        if(urlSistemaExists && sistemaExists.Url != request.Url)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaUrlExists);
        }

        //TODO: 

        sistemaExists.Update(
            request.Nombre!,
            request.Logo!,
            request.Url!,
            request.Orden!
        );

        _sistemaRepository.Update(sistemaExists);
        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(sistemaExists.Id!.Value, Message.Update);
    }
}