using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Roles.RegisterRoles;

internal sealed class RegisterRolesCommandHandler : ICommandHandler<RegisterRolesCommand, Guid>
{

    private readonly IRolRepository _rolRepository;
    private readonly ILicenciaRepository _licenciaRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public RegisterRolesCommandHandler(
        IRolRepository rolRepository,
        ILicenciaRepository licenciaRepository,
        IParametroRepository parametroRepository,
        IUnitOfWorkTenant unitOfWorkTenant
    )
    {
        _rolRepository = rolRepository;
        _licenciaRepository = licenciaRepository;
        _parametroRepository = parametroRepository;
        _unitOfWorkTenant = unitOfWorkTenant;
    }

    public async Task<Result<Guid>> Handle(RegisterRolesCommand request, CancellationToken cancellationToken)
    {
        var tipoRolExists = await _parametroRepository.GetByIdAsync(request.TipoRolId,cancellationToken);

        if(tipoRolExists is null)
        {
            return Result.Failure<Guid>(ParametroErrors.ParametroNotFound);
        }

        var nombreExists= await _rolRepository.GetByNombreAsync(request.Nombre,cancellationToken);

        if(nombreExists)
        {
            return Result.Failure<Guid>(RolErrors.AlreadyExists);
        }

        if(request.TipoRolId.Value == TipoRol.Licencia)
        {
            var licenciaId = new LicenciaId(Guid.Parse(request.LicenciaId!));

            var licenciaExists = await _licenciaRepository.GetByIdAsync(licenciaId,cancellationToken);

            if(licenciaExists is null)
            {
                return Result.Failure<Guid>(Error.NotFound);
            }

            var rolLicenciaExists = await _rolRepository.GetRolByParametroAndLicencia(request.TipoRolId,licenciaId,cancellationToken);

            if(rolLicenciaExists is not null){
                return Result.Failure<Guid>(RolErrors.AlreadyExists);
            }

            var newRolWithLicence = Rol.Create(
                request.Nombre,
                request.TipoRolId,
                licenciaId
             );

            _rolRepository.Add(newRolWithLicence);

            await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);
            return Result.Success(newRolWithLicence.Id!.Value, Message.Create);
        }      

        var newRol = Rol.Create(
                request.Nombre,
                request.TipoRolId,
                null
        );

        _rolRepository.Add(newRol);

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(newRol.Id!.Value, Message.Create);
    }
}