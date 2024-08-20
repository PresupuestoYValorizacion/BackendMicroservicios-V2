using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Parametros.GetByIdParametro;

internal sealed class GetByIdParametroQueryHandler : IQueryHandler<GetByIdParametroQuery, ParametroDto>
{
    private readonly IParametroRepository _parametroRepository;

    private readonly IMapper _mapper;

    public GetByIdParametroQueryHandler(
        IParametroRepository parametroRepository,
        IMapper mapper
    )
    {
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<ParametroDto>> Handle(GetByIdParametroQuery request, CancellationToken cancellationToken)
    {
        var parametro = await _parametroRepository.GetByIdAsync(new ParametroId(request.Id), cancellationToken);

        if(parametro is null){
            return Result.Failure<ParametroDto>(ParametroErrors.ParametroNotFound)!;
        }

        var parametroDto = _mapper.Map<ParametroDto>(parametro);

        return parametroDto!;
    }
}