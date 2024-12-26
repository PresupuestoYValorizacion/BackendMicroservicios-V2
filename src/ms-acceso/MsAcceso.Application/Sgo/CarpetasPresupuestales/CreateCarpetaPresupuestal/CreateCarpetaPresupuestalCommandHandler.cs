using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.CreateCarpetaPresupuestal;

internal class CreateCarpetaPresupuestalCommandHandler : ICommandHandler<CreateCarpetaPresupuestalCommand, Guid>
{
    private readonly ICarpetaPresupuestalTenantRepository _carpetaPresupuestalRepository;

    public CreateCarpetaPresupuestalCommandHandler(
        ICarpetaPresupuestalTenantRepository carpetaPresupuestalRepository
    )
    {
        _carpetaPresupuestalRepository = carpetaPresupuestalRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCarpetaPresupuestalCommand request, CancellationToken cancellationToken)
    {
        var dependencia = request.Dependencia.Length == 0 ? null : request.Dependencia;
        var carpetaPresupuestalExiste = await _carpetaPresupuestalRepository.GetByNombreAsync(request.Nombre, request.Nivel, dependencia, cancellationToken);

        if (carpetaPresupuestalExiste is not null)
        {
            return Result.Failure<Guid>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalNameExists);
        }

        var newCarpetaPresupuestal = CarpetaPresupuestalTenant.Create(
                request.Dependencia.Length == 0 ? null : new CarpetaPresupuestalTenantId(Guid.Parse(request.Dependencia)),
                request.Nombre,
                request.Nivel
        );

        _carpetaPresupuestalRepository.Add(newCarpetaPresupuestal);

        await _carpetaPresupuestalRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(newCarpetaPresupuestal.Id!.Value, Message.Create);



    }

}