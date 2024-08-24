using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.GetSistemasById;

internal sealed class GetSistemasByIdQueryHandler : IQueryHandler<GetSistemasByIdQuery, SistemasDto?>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMapper _mapper;

    public GetSistemasByIdQueryHandler(
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<SistemasDto?>> Handle(GetSistemasByIdQuery request, CancellationToken cancellationToken)
    {
        var sistemaId = Guid.Parse(request.Id!);
        var sistema = await _sistemaRepository.GetByIdAsync(new SistemaId(sistemaId),cancellationToken);

        if(sistema is null)
        {
            return Result.Failure<SistemasDto>(SistemaErrors.SistemaNotFound);
        }

        var sistemaDto = _mapper.Map<SistemasDto>(sistema);

        return sistemaDto;
    }
}