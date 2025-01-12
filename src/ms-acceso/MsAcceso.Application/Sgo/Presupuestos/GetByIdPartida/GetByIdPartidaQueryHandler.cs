using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetByIdPartida;


internal sealed class GetByIdPartidaQueryHandler : IQueryHandler<GetByIdPartidaQuery, PartidaTenantDto>
{
    private readonly IPartidaTenantRepository _partidaRepository;

    private readonly IMapper _mapper;

    public GetByIdPartidaQueryHandler(
        IPartidaTenantRepository partidaRepository,
        IMapper mapper
    )
    {
        _partidaRepository = partidaRepository;
        _mapper = mapper;
    }

    public async Task<Result<PartidaTenantDto>> Handle(GetByIdPartidaQuery request, CancellationToken cancellationToken)
    {

        var partida = await _partidaRepository.GetByIdAsync(new PartidaTenantId(Guid.Parse(request.Id)), cancellationToken);

        if (partida is null)
        {
            return Result.Failure<PartidaTenantDto>(PartidaTenantErrors.PartidaNotFound)!;
        }

        var partidaDto = _mapper.Map<PartidaTenantDto>(partida);

        return partidaDto!;


    }
}