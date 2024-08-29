using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.UpdateLicencias;

internal sealed class UpdateLicenciasCommandHandler : ICommandHandler<UpdateLicenciasCommand, Guid>
{
    private readonly ILicenciaRepository _licenciaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public UpdateLicenciasCommandHandler(
        ILicenciaRepository licenciaRepository,
        IUnitOfWorkTenant unitOfWork)
        {
            _licenciaRepository = licenciaRepository;
            _unitOfWork = unitOfWork;
        }

    public async Task<Result<Guid>> Handle(UpdateLicenciasCommand request, CancellationToken cancellationToken)
    {

        var licencia = await _licenciaRepository.GetByIdAsync(request.Id, cancellationToken);

        if(licencia is null)
        {
            return Result.Failure<Guid>(LicenciaErrors.NotFound)!;
        }

        if(licencia.Nombre != request.Nombre){

            var licenciaExists = await _licenciaRepository.LicenciaExists(request.Nombre!, cancellationToken); 

            if(licenciaExists)
            {
                return Result.Failure<Guid>(LicenciaErrors.LicenciaExists);
            }
        }

        licencia.Update(
            request.Nombre!
        );

        _licenciaRepository.Update(licencia);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(licencia.Id!.Value, Message.Update);
    }
}