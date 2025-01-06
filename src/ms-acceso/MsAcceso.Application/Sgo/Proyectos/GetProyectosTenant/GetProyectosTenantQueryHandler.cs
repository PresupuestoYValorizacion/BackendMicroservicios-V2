using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;


namespace MsAcceso.Application.Sgo.Proyectos.GetProyectosTenant;

internal sealed class GetProyectosTenantQueryHandler : IQueryHandler<GetProyectosTenantQuery, List<ProyectoTenantDto>>
{
    private readonly IProyectoTenantRepository _proyectoRepository;
    private readonly IEspecialidadTenantRepository _especialidadRepository;
    private readonly IParametroRepository _parametroRepository;

    private readonly IMapper _mapper;

    public GetProyectosTenantQueryHandler(
        IProyectoTenantRepository proyectoRepository,
        IEspecialidadTenantRepository especialidadRepository,
        IParametroRepository parametroRepository,
        IMapper mapper
    )
    {
        _proyectoRepository = proyectoRepository;
        _especialidadRepository = especialidadRepository;
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProyectoTenantDto>>> Handle(GetProyectosTenantQuery request, CancellationToken cancellationToken)
    {
        var proyectos = await _proyectoRepository.GetAllAsyncWithIncludes(cancellationToken);

        var departamentos = await _parametroRepository.GetRelatedEntitiesAsync( ParametroEnum.Ubigeo, cancellationToken);

        foreach(var proyecto in proyectos)
        {  
            if(proyecto.Presupuesto is null)
            {
                continue;
            }

            proyecto.Presupuesto!.Departamento = departamentos.FirstOrDefault(x => x.Id == new ParametroId(proyecto.Presupuesto.DepartamentoId ?? 0));
            proyecto.Presupuesto!.Provincia = await _parametroRepository.GetByIdAsync(new ParametroId(proyecto.Presupuesto.ProvinciaId ?? 0), cancellationToken);
            proyecto.Presupuesto!.Distrito = await _parametroRepository.GetByIdAsync(new ParametroId(proyecto.Presupuesto.DistritoId ?? 0), cancellationToken);

            // cliente.TipoPersona = tipoPersonas.FirstOrDefault(x => x.Id == new ParametroId(cliente.TipoPersonaId ?? 0));
            // cliente.TipoCliente = tipoClientes.FirstOrDefault(x => x.Id == new ParametroId(cliente.TipoClienteId ?? 0));

        }

        var proyectosDto = _mapper.Map<List<ProyectoTenantDto>>(proyectos);

        return proyectosDto!;
        
    }


}