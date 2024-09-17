using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Sistemas.GetSistemasByDependencia;

internal sealed class GetSistemasByDependenciaQueryHandler : IQueryHandler<GetSistemasByDependenciaQuery, List<SistemaDto>>
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

    public async Task<Result<List<SistemaDto>>> Handle(GetSistemasByDependenciaQuery request, CancellationToken cancellationToken)
    {
        var sistemas = await _sistemaRepository.GetSistemasByDependencia(request.Dependencia!, cancellationToken);

        var sistemasDto = _mapper.Map<List<SistemaDto>>(sistemas);

        return sistemasDto!;
    }


}