using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;


namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.GetCarpetasPresupuestales;

internal sealed class GetCarpetasPresupuestalesQueryHandler : IQueryHandler<GetCarpetasPresupuestalesQuery, List<CarpetaPresupuestalTenantDto>>
{
    private readonly ICarpetaPresupuestalTenantRepository _carpetaPresupuestalRepository;
    private readonly IMapper _mapper;

    public GetCarpetasPresupuestalesQueryHandler(
        ICarpetaPresupuestalTenantRepository carpetaPresupuestalRepository,
        IMapper mapper
    )
    {
        _carpetaPresupuestalRepository = carpetaPresupuestalRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CarpetaPresupuestalTenantDto>>> Handle(GetCarpetasPresupuestalesQuery request, CancellationToken cancellationToken)
    {

        var carpetasPresupuestales = await _carpetaPresupuestalRepository.GetAllCarpetaPresupuestales(cancellationToken);

        var carpetasPresupuestalesaDto = _mapper.Map<List<CarpetaPresupuestalTenantDto>>(carpetasPresupuestales);

        return carpetasPresupuestalesaDto!;
        
    }


}