using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetAllRecursosTenant;


internal sealed class GetAllRecursosTenantQueryHandler : IQueryHandler<GetAllRecursosTenantQuery, List<RecursoTenantDto>>
{
    private readonly IRecursoTenantRepository _recursoRepository;

    private readonly IMapper _mapper;

    public GetAllRecursosTenantQueryHandler(
        IRecursoTenantRepository recursoRepository,
        IMapper mapper
    )
    {
        _recursoRepository = recursoRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RecursoTenantDto>>> Handle(GetAllRecursosTenantQuery request, CancellationToken cancellationToken)
    {
        var recursos = await _recursoRepository.GetAllByUnidadMedidaAsync(request.UnidadMedidaId, cancellationToken);

        var recursosDto = _mapper.Map<List<RecursoTenantDto>>(recursos);

        return recursosDto!;

    }
}