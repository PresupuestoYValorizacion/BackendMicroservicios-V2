using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.GetByIdCarpetaPresupuestal;


internal sealed class GetByIdCarpetaPresupuestalQueryHandler : IQueryHandler<GetByIdCarpetaPresupuestalQuery, CarpetaPresupuestalTenantDto>
{
    private readonly ICarpetaPresupuestalTenantRepository _carpetaPresupuestalRepository;

    private readonly IMapper _mapper;

    public GetByIdCarpetaPresupuestalQueryHandler(
        ICarpetaPresupuestalTenantRepository carpetaPresupuestalRepository,
        IMapper mapper
    )
    {
        _carpetaPresupuestalRepository = carpetaPresupuestalRepository;
        _mapper = mapper;
    }

    public async Task<Result<CarpetaPresupuestalTenantDto>> Handle(GetByIdCarpetaPresupuestalQuery request, CancellationToken cancellationToken)
    {
        
        var carpetaPresupuestal = await _carpetaPresupuestalRepository.GetByIdAsync(new CarpetaPresupuestalTenantId( Guid.Parse(request.Id)), cancellationToken);

        if(carpetaPresupuestal is null){
            return Result.Failure<CarpetaPresupuestalTenantDto>(CarpetaPresupuestalTenantErrors.CarpetaPresupuestalNotFound)!;
        }

        var carpetaPresupuestalDto = _mapper.Map<CarpetaPresupuestalTenantDto>(carpetaPresupuestal);

        return carpetaPresupuestalDto!;
        
    }
}