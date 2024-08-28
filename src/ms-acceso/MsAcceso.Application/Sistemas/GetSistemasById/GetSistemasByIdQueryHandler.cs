using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Sistemas.GetSistemasById;

internal sealed class GetSistemasByIdQueryHandler : IQueryHandler<GetSistemasByIdQuery, SistemaDto?>
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

    public async Task<Result<SistemaDto?>> Handle(GetSistemasByIdQuery request, CancellationToken cancellationToken)
    {
        var sistemaId = Guid.Parse(request.Id!);
        var sistema = await _sistemaRepository.SistemaGetByIdAsync(new SistemaId(sistemaId),cancellationToken);

        if(sistema is null)
        {
            return Result.Failure<SistemaDto>(SistemaErrors.SistemaNotFound);
        }

        var sistemaDto = _mapper.Map<SistemaDto>(sistema);

        return sistemaDto;
    }
}