using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Root.Roles.UpdateRoles;

internal sealed class UpdateRolesCommandHandler : ICommandHandler<UpdateRolesCommand, Guid>
{

    private readonly IRolRepository _rolRepository;
    private readonly ILicenciaRepository _licenciaRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IUnitOfWorkTenant _unitOfWorkTenant;

    public UpdateRolesCommandHandler(
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

    public async Task<Result<Guid>> Handle(UpdateRolesCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByIdAsync(request.RolId,cancellationToken);

        if(rol is null)
        {
            return Result.Failure<Guid>(RolErrors.RolNotExists);
        }

        var tipoRolExists = await _parametroRepository.GetByIdAsync(request.TipoRolId,cancellationToken);

        if(tipoRolExists is null)
        {
            return Result.Failure<Guid>(ParametroErrors.ParametroNotFound);
        }

        if(rol.Nombre != request.Nombre)
        {
            var nombreExists= await _rolRepository.GetByNombreAsync(request.Nombre,cancellationToken);

            if(nombreExists)
            {
                return Result.Failure<Guid>(RolErrors.AlreadyExists);
            }

        }


        if(request.TipoRolId.Value == TipoRol.Licencia && rol.LicenciaId != request.LicenciaId)
        {

            var licenciaExists = await _licenciaRepository.GetByIdAsync(request.LicenciaId!,cancellationToken);

            if(licenciaExists is null)
            {
                return Result.Failure<Guid>(Error.NotFound);
            }

            var rolLicenciaExists = await _rolRepository.GetRolByParametroAndLicencia(request.TipoRolId,request.LicenciaId!,cancellationToken);

            if(rolLicenciaExists is not null){
                return Result.Failure<Guid>(RolErrors.AlreadyExists);
            }

            rol.Update(
                request.Nombre,
                request.TipoRolId,
                request.LicenciaId
            );

        }
        else if(request.TipoRolId.Value == TipoRol.Administrador)
        {
            rol.Update(
                request.Nombre,
                request.TipoRolId,
                null
            );
            
        }

        _rolRepository.Update(rol);

        await _unitOfWorkTenant.SaveChangesAsync(cancellationToken);

        return Result.Success(rol.Id!.Value, Message.Update);
    }
}