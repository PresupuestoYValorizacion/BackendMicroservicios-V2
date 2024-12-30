using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;


namespace MsAcceso.Application.Sgo.Proyectos.GetProyectosTenant;

internal sealed class GetProyectosTenantQueryHandler : IQueryHandler<GetProyectosTenantQuery, List<ProyectoTenantDto>>
{
    private readonly IProyectoTenantRepository _proyectoRepository;
    private readonly IEspecialidadTenantRepository _especialidadRepository;
    private readonly IMapper _mapper;

    public GetProyectosTenantQueryHandler(
        IProyectoTenantRepository proyectoRepository,
        IEspecialidadTenantRepository especialidadRepository,
        IMapper mapper
    )
    {
        _proyectoRepository = proyectoRepository;
        _especialidadRepository = especialidadRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProyectoTenantDto>>> Handle(GetProyectosTenantQuery request, CancellationToken cancellationToken)
    {
        var proyectos = await _proyectoRepository.GetAllAsyncWithIncludes(cancellationToken);

        var proyectosDto = _mapper.Map<List<ProyectoTenantDto>>(proyectos);

        return proyectosDto!;
        
    }


}