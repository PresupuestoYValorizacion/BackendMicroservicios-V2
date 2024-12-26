using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.DeleteCarpetaPresupuestal;

internal sealed class DeleteCarpetaPresupuestalCommandHandler : ICommandHandler<DeleteCarpetaPresupuestalCommand, Guid>
{
    private readonly ICarpetaPresupuestalTenantRepository _carpetaPresupuestalRepository;

    public DeleteCarpetaPresupuestalCommandHandler(
        ICarpetaPresupuestalTenantRepository carpetaPresupuestalRepository
    )
    {
        _carpetaPresupuestalRepository = carpetaPresupuestalRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteCarpetaPresupuestalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var carpetaPresupuestal = await _carpetaPresupuestalRepository.GetByIdAsync(request.Id, cancellationToken);

            if (carpetaPresupuestal is null)
            {
                return Result.Failure<Guid>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalNotFound);
            }

            _carpetaPresupuestalRepository.Delete(carpetaPresupuestal);
            
            await _carpetaPresupuestalRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(carpetaPresupuestal.Id!.Value!, Message.Delete);

        }
        catch (Exception ex) when (ExceptionSql.IsForeignKeyViolation(ex))
        {
            return Result.Failure<Guid>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalInUse);

        }

    }
}