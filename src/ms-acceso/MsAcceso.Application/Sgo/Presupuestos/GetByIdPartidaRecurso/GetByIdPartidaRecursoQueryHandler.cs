using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetByIdPartidaRecurso;


internal sealed class GetByIdPartidaRecursoQueryHandler : IQueryHandler<GetByIdPartidaRecursoQuery, PartidaRecursoTenantDto>
{
    private readonly IPartidaRecursoTenantRepository _partidaRecursoRepository;

    private readonly IMapper _mapper;

    public GetByIdPartidaRecursoQueryHandler(
        IPartidaRecursoTenantRepository partidaRecursoRepository,
        IMapper mapper
    )
    {
        _partidaRecursoRepository = partidaRecursoRepository;
        _mapper = mapper;
    }

    public async Task<Result<PartidaRecursoTenantDto>> Handle(GetByIdPartidaRecursoQuery request, CancellationToken cancellationToken)
    {

        var partidaRecurso = await _partidaRecursoRepository.GetByIdWithIncludesAsync(new PartidaRecursoTenantId(Guid.Parse(request.Id)), cancellationToken);

        if (partidaRecurso is null)
        {
            return Result.Failure<PartidaRecursoTenantDto>(PartidaRecursoTenantErrors.NotFound)!;
        }

        var partidaRecursoDto = _mapper.Map<PartidaRecursoTenantDto>(partidaRecurso);

        return partidaRecursoDto!;


    }
}