using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.DeleteLicencias;

internal sealed class DeleteLicenciasCommandHandler : ICommandHandler<DeleteLicenciasCommand, Guid>
{
    private readonly ILicenciaRepository _licenciaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DeleteLicenciasCommandHandler(
        ILicenciaRepository licenciaRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _licenciaRepository = licenciaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteLicenciasCommand request, CancellationToken cancellationToken)
    {
        var licencia = await _licenciaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (licencia is null)
        {
            return Result.Failure<Guid>(LicenciaErrors.NotFound)!;
        }

        _licenciaRepository.Delete(licencia);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(licencia.Id!.Value, Message.Delete);
    }
}