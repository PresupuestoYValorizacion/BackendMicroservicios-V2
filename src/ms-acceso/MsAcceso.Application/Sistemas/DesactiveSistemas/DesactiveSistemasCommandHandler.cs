using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.DesactiveSistemas;

internal sealed class DesactiveSistemasCommandHandler : ICommandHandler<DesactiveSistemasCommand, Guid>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public DesactiveSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }
    
    public async Task<Result<Guid>> Handle(DesactiveSistemasCommand request, CancellationToken cancellationToken)
    {
        var sistemaId = new SistemaId(Guid.Parse(request.Id));

        var sistemaExists = await _sistemaRepository.SistemaGetByIdAsync(sistemaId,cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var sistemasDependientes = await _sistemaRepository.GetAllSistemasBySubnivel(sistemaExists.Id!,cancellationToken);

        foreach(var sistemaDependiente in sistemasDependientes)
        {
            sistemaDependiente.Desactive();
            _sistemaRepository.Update(sistemaDependiente);
        }

        sistemaExists.Desactive();

        _sistemaRepository.Update(sistemaExists);

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(sistemaExists.Id!.Value, Message.Desactivate);
    }
}