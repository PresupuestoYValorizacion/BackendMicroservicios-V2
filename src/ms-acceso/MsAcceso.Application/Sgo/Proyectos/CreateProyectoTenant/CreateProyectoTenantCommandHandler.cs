using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.CreateProyectoTenant;

internal class CreateProyectoTenantCommandHandler : ICommandHandler<CreateProyectoTenantCommand, Guid>
{
    private readonly IProyectoTenantRepository _proyectoRepository;

    public CreateProyectoTenantCommandHandler(
        IProyectoTenantRepository proyectoRepository
    )
    {
        _proyectoRepository = proyectoRepository;
    }

    public async Task<Result<Guid>> Handle(CreateProyectoTenantCommand request, CancellationToken cancellationToken)
    {
       var proyectoExiste = await _proyectoRepository.GetByNombre(request.Nombre,cancellationToken);

        if(proyectoExiste is not null)
        {
            return Result.Failure<Guid>(ProyectoTenantErrors.ProyectoExists);
        }

        var newProyecto = ProyectoTenant.Create(
                request.Nombre
        );

        _proyectoRepository.Add(newProyecto);

        await _proyectoRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newProyecto.Id!.Value, Message.Create);

    }

}