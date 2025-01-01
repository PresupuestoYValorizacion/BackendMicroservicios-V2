using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.DeleteProyectoTenant;

internal sealed class DeleteProyectoTenantCommandHandler : ICommandHandler<DeleteProyectoTenantCommand, Guid>
{
    private readonly IProyectoTenantRepository _proyectoRepository;

    public DeleteProyectoTenantCommandHandler(
        IProyectoTenantRepository proyectoRepository
    )
    {
        _proyectoRepository = proyectoRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteProyectoTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var proyecto = await _proyectoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (proyecto is null)
            {
                return Result.Failure<Guid>(ProyectoTenantErrors.ProyectoNotExists);
            }

            _proyectoRepository.Delete(proyecto);
            
            await _proyectoRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(proyecto.Id!.Value!, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(ProyectoTenantErrors.ProyectoInUse);

        }

    }
}