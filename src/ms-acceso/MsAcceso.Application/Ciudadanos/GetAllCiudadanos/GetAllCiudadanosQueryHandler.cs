using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Ciudadanos;


namespace MsAcceso.Application.Ciudadanos.GetAllCiudadanos;


internal sealed class GetAllCiudadanosQueryHandler : IQueryHandler<GetAllCiudadanosQuery, List<CiudadanoDto>>
{
    private readonly ICiudadanoRepository _ciudadanoRepository;

    private readonly IMapper _mapper;

    public GetAllCiudadanosQueryHandler(
        ICiudadanoRepository ciudadanoRepository,
        IMapper mapper
    )
    {
        _ciudadanoRepository = ciudadanoRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CiudadanoDto>>> Handle(GetAllCiudadanosQuery request, CancellationToken cancellationToken)
    {
        var ciudadanos = await _ciudadanoRepository.GetAll();

        var ciudadanosDto = _mapper.Map<List<CiudadanoDto>>(ciudadanos);

        return ciudadanosDto!;

    }
}