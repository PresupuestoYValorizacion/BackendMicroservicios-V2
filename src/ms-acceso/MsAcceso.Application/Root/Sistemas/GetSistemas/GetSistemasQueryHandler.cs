using AutoMapper;
using LinqKit;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Paginations;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Sistemas.GetSistemas;

internal sealed class GetSistemasQueryHandler : IQueryHandler<GetSistemasQuery, List<SistemaDto>>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMapper _mapper;

    public GetSistemasQueryHandler(
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemaDto>>> Handle(GetSistemasQuery request, CancellationToken cancellationToken)
    {
        var sistemas = await _sistemaRepository.GetAllSistemas(cancellationToken);

        var SistemaDto = _mapper.Map<List<SistemaDto>>(sistemas);

        return SistemaDto!;
    }


}