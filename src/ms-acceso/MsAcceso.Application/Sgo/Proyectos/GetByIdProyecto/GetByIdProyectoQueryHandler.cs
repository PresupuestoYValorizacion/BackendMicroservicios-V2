using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetByIdProyecto;


internal sealed class GetByIdProyectoQueryHandler : IQueryHandler<GetByIdProyectoQuery, ProyectoTenantDto>
{
    private readonly IProyectoTenantRepository _proyectoRepository;

    private readonly IMapper _mapper;

    public GetByIdProyectoQueryHandler(
        IProyectoTenantRepository proyectoRepository,
        IMapper mapper
    )
    {
        _proyectoRepository = proyectoRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProyectoTenantDto>> Handle(GetByIdProyectoQuery request, CancellationToken cancellationToken)
    {

        var proyecto = await _proyectoRepository.GetByIdAsync(new ProyectoTenantId(Guid.Parse(request.Id)), cancellationToken);

        if (proyecto is null)
        {
            return Result.Failure<ProyectoTenantDto>(ProyectoTenantErrors.NotFound)!;
        }

        var proyectoDto = _mapper.Map<ProyectoTenantDto>(proyecto);

        return proyectoDto!;


    }
}