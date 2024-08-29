using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Pruebas.RegisterLicencias;

internal sealed class RegisterLicenciasCommandHandler : ICommandHandler<RegisterLicenciasCommand, Guid>
{
    private readonly ILicenciaRepository _licenciaRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public RegisterLicenciasCommandHandler(
        ILicenciaRepository licenciaRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _licenciaRepository = licenciaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterLicenciasCommand request, CancellationToken cancellationToken)
    {

        var nombre = request.Nombre;
        var licenciaId = LicenciaId.New();

        var licenciaExists = await _licenciaRepository.LicenciaExists(nombre, cancellationToken); 

        if(licenciaExists)
        {
            return Result.Failure<Guid>(LicenciaErrors.LicenciaExists);
        }

        var licencia = Licencia.Create(
            licenciaId,
            nombre
        );

        _licenciaRepository.Add(licencia);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(licencia.Id!.Value, Message.Create);
    }
}