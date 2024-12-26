using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.UpdateCarpetaPresupuestal;

internal sealed class UpdateCarpetaPresupuestalCommandHandler : ICommandHandler<UpdateCarpetaPresupuestalCommand, Guid>
{
    private readonly ICarpetaPresupuestalTenantRepository _carpetaPresupuestalRepository;

    public UpdateCarpetaPresupuestalCommandHandler(
        ICarpetaPresupuestalTenantRepository carpetaPresupuestalRepository)
    {
        _carpetaPresupuestalRepository = carpetaPresupuestalRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateCarpetaPresupuestalCommand request, CancellationToken cancellationToken)
    {

        var carpetaPresupuestal = await _carpetaPresupuestalRepository.GetByIdAsync(new CarpetaPresupuestalTenantId(Guid.Parse(request.Id)), cancellationToken);

        if (carpetaPresupuestal is null)
        {
            return Result.Failure<Guid>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalNotFound);
        }

        if (carpetaPresupuestal.Nombre != request.Nombre)
        {

            var nombreExists = await _carpetaPresupuestalRepository.GetByNombreAsync(request.Nombre, carpetaPresupuestal.Nivel! ?? 0, carpetaPresupuestal.Dependencia != null ? carpetaPresupuestal.Dependencia.Value.ToString() : null, cancellationToken);

            if (nombreExists is not null)
            {
                return Result.Failure<Guid>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalNameExists);
            }
        }

        carpetaPresupuestal.Update(
            request.Nombre);

        _carpetaPresupuestalRepository.Update(carpetaPresupuestal);

        await _carpetaPresupuestalRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(carpetaPresupuestal.Id!.Value, Message.Update);
    }
}