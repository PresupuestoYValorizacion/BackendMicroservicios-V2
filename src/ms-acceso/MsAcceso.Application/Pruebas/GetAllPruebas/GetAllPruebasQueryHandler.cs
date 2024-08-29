using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;


namespace MsAcceso.Application.Pruebas.GetAllPruebas;


internal sealed class GetAllPruebasQueryHandler : IQueryHandler<GetAllPruebasQuery, List<LicenciaDto>>
{
    private readonly ILicenciaRepository _licenciaRepository;

    private readonly IMapper _mapper;

    public GetAllPruebasQueryHandler(
        ILicenciaRepository licenciaRepository,
        IMapper mapper
    )
    {
        _licenciaRepository = licenciaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<LicenciaDto>>> Handle(GetAllPruebasQuery request, CancellationToken cancellationToken)
    {
        var licencias = await _licenciaRepository.GetAll();

        var licenciaDto = _mapper.Map<List<LicenciaDto>>(licencias);

        return licenciaDto!;

    }
}