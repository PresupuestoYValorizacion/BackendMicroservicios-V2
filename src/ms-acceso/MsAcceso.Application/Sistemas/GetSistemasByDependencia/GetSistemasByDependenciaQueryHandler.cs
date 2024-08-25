using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.GetSistemasByDependencia;

internal sealed class GetSistemasByDependenciaQueryHandler : IQueryHandler<GetSistemasByDependenciaQuery, List<SistemasDto>?>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMapper _mapper;

    public GetSistemasByDependenciaQueryHandler(
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemasDto>?>> Handle(GetSistemasByDependenciaQuery request, CancellationToken cancellationToken)
    {
        var sistemaId = Guid.Parse(request.Dependencia!);

        var sistemaExists = await _sistemaRepository.GetByIdAsync(new SistemaId(sistemaId),cancellationToken);

        if(sistemaExists is null)
        {
            return Result.Failure<List<SistemasDto>>(SistemaErrors.SistemaNotFound);
        }

        var sistemas = await _sistemaRepository.GetAllSistemasBySubnivel(sistemaExists.Id!,cancellationToken);

        var sistemasDto = _mapper.Map<List<SistemasDto>>(sistemas);

        return sistemasDto!;
    }
}