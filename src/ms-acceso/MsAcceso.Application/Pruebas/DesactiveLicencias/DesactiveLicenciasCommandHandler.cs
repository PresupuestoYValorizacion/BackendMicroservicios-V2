using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.DesactiveLicencias;

internal sealed class DesactiveLicenciasCommandHandler : ICommandHandler<DesactiveLicenciasCommand, Guid>
{
    private readonly ILicenciaRepository _licenciaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DesactiveLicenciasCommandHandler(
        ILicenciaRepository licenciaRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _licenciaRepository = licenciaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DesactiveLicenciasCommand request, CancellationToken cancellationToken)
    {
        var licencia = await _licenciaRepository.GetByIdAsync(request.Id,cancellationToken);

        if(licencia is null)
        {
            return Result.Failure<Guid>(LicenciaErrors.NotFound)!;
        }

        licencia.Desactive();

        _licenciaRepository.Update(licencia);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(licencia.Id!.Value, Message.Desactivate);
    }
}