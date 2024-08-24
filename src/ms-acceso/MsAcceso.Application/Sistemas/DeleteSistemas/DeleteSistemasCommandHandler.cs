using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.DeleteSistemas;

internal sealed class DeleteSistemasCommandHandler : ICommandHandler<DeleteSistemasCommand, Guid>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public DeleteSistemasCommandHandler(
        ISistemaRepository sistemaRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _sistemaRepository = sistemaRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(DeleteSistemasCommand request, CancellationToken cancellationToken)
    {
        var sistemaId = new SistemaId(Guid.Parse(request.Id));
        
        var sistemaExists = await _sistemaRepository.GetByIdAsync(sistemaId,cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<Guid>(SistemaErrors.SistemaNotFound);
        }

        var sistemasDependientes = await _sistemaRepository.GetAllSistemasBySubnivel(sistemaId,cancellationToken);

        foreach(var sistemaDependiente in sistemasDependientes)
        {
            _sistemaRepository.Delete(sistemaDependiente);
        }
        
        _sistemaRepository.Delete(sistemaExists);

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(sistemaExists.Id!.Value, Message.Delete);
    }
}