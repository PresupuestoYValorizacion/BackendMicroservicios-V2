using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Application.Sgo.Proyectos.DeleteEspecialidadTenant;

internal sealed class DeleteEspecialidadTenantCommandHandler : ICommandHandler<DeleteEspecialidadTenantCommand, Guid>
{
    private readonly IEspecialidadTenantRepository _especialidadRepository;

    public DeleteEspecialidadTenantCommandHandler(
        IEspecialidadTenantRepository especialidadRepository
    )
    {
        _especialidadRepository = especialidadRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteEspecialidadTenantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var especialidad = await _especialidadRepository.GetByIdAsync(request.Id, cancellationToken);

            if (especialidad is null)
            {
                return Result.Failure<Guid>(EspecialidadTenantErrors.EspecialidadNotExists);
            }

            _especialidadRepository.Delete(especialidad);
            
            await _especialidadRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(especialidad.Id!.Value!, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(EspecialidadTenantErrors.EspecialidadInUse);

        }

    }
}