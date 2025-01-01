using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.CreateEspecialidadTenant;


internal class CreateEspecialidadTenantCommandHandler : ICommandHandler<CreateEspecialidadTenantCommand, Guid>
{
    private readonly IEspecialidadTenantRepository _especialidadRepository;
    private readonly IPresupuestoEspecialidadTenantRepository _presupuestoEspecialidadRepository;

    public CreateEspecialidadTenantCommandHandler(
        IEspecialidadTenantRepository especialidadRepository,
        IPresupuestoEspecialidadTenantRepository presupuestoEspecialidadRepository
    )
    {
        _especialidadRepository = especialidadRepository;
        _presupuestoEspecialidadRepository = presupuestoEspecialidadRepository;
    }

    public async Task<Result<Guid>> Handle(CreateEspecialidadTenantCommand request, CancellationToken cancellationToken)
    {
       var especialidadExiste = await _especialidadRepository.GetByNombreAsync(request.Nombre,cancellationToken);

        if(especialidadExiste is not null)
        {
            return Result.Failure<Guid>(EspecialidadTenantErrors.EspecialidadExists);
        }

        var newEspecialidad = EspecialidadTenant.Create(
                request.Nombre,
                new ProyectoTenantId(Guid.Parse(request.ProyectoId))
        );

        _especialidadRepository.Add(newEspecialidad);

        // var newPresupuestoEspecialidad = PresupuestoEspecialidadTenant.Create(newEspecialidad.Id!, Guid.NewGuid().ToString());

        // _presupuestoEspecialidadRepository.Add(newPresupuestoEspecialidad);

        await _especialidadRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newEspecialidad.Id!.Value, Message.Create);

    }

   
}