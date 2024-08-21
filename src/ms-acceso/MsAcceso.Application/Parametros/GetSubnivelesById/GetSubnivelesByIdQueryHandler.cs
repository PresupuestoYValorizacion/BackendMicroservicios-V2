using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.GetSubnivelesById;

internal sealed class GetSubnivelesByIdQueryHandler : IQueryHandler<GetSubnivelesByIdQuery, List<ParametroDto>>
{
    private readonly IParametroRepository _parametroRepository;

    private readonly IMapper _mapper;

    public GetSubnivelesByIdQueryHandler(
        IParametroRepository parametroRepository,
        IMapper mapper
    )
    {
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ParametroDto>>> Handle(GetSubnivelesByIdQuery request, CancellationToken cancellationToken)
    {
        var parametro = await _parametroRepository.GetByIdAsync(new ParametroId(request.Id), cancellationToken);

        if(parametro is null){
            return Result.Failure<List<ParametroDto>>(ParametroErrors.ParametroNotFound)!;
        }

        var parametros= await _parametroRepository.GetRelatedEntitiesAsync(request.Id, cancellationToken);

        var parametrosDto = _mapper.Map<List<ParametroDto>>(parametros);

        return parametrosDto!;
    }
}