using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetTitulosByEspecialidad;

internal sealed class GetTitulosByEspecialidadQueryHandler : IQueryHandler<GetTitulosByEspecialidadQuery, List<PresupuestoEspecialidadTituloTenantDto>>
{
    private readonly IProyectoTenantRepository _proyectoRepository;
    private readonly IPresupuestoEspecialidadTituloTenantRepository _presupuestoEspecialidadTituloRepository;

    private readonly IEspecialidadTenantRepository _especialidadRepository;
    private readonly IParametroRepository _parametroRepository;

    private readonly IMapper _mapper;

    public GetTitulosByEspecialidadQueryHandler(
        IProyectoTenantRepository proyectoRepository,
        IEspecialidadTenantRepository especialidadRepository,
        IPresupuestoEspecialidadTituloTenantRepository presupuestoEspecialidadTituloRepository,
        IParametroRepository parametroRepository,
        IMapper mapper
    )
    {
        _presupuestoEspecialidadTituloRepository = presupuestoEspecialidadTituloRepository;
        _proyectoRepository = proyectoRepository;
        _especialidadRepository = especialidadRepository;
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<PresupuestoEspecialidadTituloTenantDto>>> Handle(GetTitulosByEspecialidadQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var especialidadTitulos = await _presupuestoEspecialidadTituloRepository.GetAllAsyncWithIncludes(new EspecialidadTenantId(Guid.Parse(request.EspecialidadId)), cancellationToken);

            var especialidadTitulosDto = _mapper.Map<List<PresupuestoEspecialidadTituloTenantDto>>(especialidadTitulos);

            return especialidadTitulosDto!;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return Result.Failure<List<PresupuestoEspecialidadTituloTenantDto>>(PresupuestoEspecialidadTituloPartidaTenantErrors.PresupuestoExists)!;

    }


}