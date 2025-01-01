using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.UpdateProyectoTenant;

internal sealed class UpdateProyectoTenantCommandHandler : ICommandHandler<UpdateProyectoTenantCommand, Guid>
{
    private readonly IProyectoTenantRepository _proyectoRepository;
    private readonly IEspecialidadTenantRepository _especialidadRepository;

    public UpdateProyectoTenantCommandHandler(
        IProyectoTenantRepository proyectoRepository,
        IEspecialidadTenantRepository especialidadRepository
        )
    {
        _proyectoRepository = proyectoRepository;
        _especialidadRepository = especialidadRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateProyectoTenantCommand request, CancellationToken cancellationToken)
    {

        if(request.IsProyecto)
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(new ProyectoTenantId(Guid.Parse(request.Id)), cancellationToken);

            if (proyecto is null)
            {
                return Result.Failure<Guid>(ProyectoTenantErrors.NotFound);
            }

            if (proyecto.Nombre != request.Nombre)
            {

                var nombreExists = await _proyectoRepository.GetByNombre(request.Nombre, cancellationToken);

                if (nombreExists is not null)
                {
                    return Result.Failure<Guid>(ProyectoTenantErrors.ProyectoExists);
                }
            }

            proyecto.Update(
                request.Nombre);

            _proyectoRepository.Update(proyecto);


        }
        else
        {

             var especialidad = await _especialidadRepository.GetByIdAsync(new EspecialidadTenantId(Guid.Parse(request.Id)), cancellationToken);

            if (especialidad is null)
            {
                return Result.Failure<Guid>(EspecialidadTenantErrors.NotFound);
            }

            if (especialidad.Nombre != request.Nombre)
            {

                var nombreExists = await _especialidadRepository.GetByNombreAsync(request.Nombre, cancellationToken);

                if (nombreExists is not null)
                {
                    return Result.Failure<Guid>(EspecialidadTenantErrors.EspecialidadExists);
                }
            }

            especialidad.Update(
                request.Nombre);

            _especialidadRepository.Update(especialidad);
            
        }

        await _proyectoRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(Guid.Empty, Message.Update);
    }
}