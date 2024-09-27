using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;


namespace MsAcceso.Application.Root.Licencias.GetAllLicencias;


internal sealed class GetAllLicenciasQueryHandler : IQueryHandler<GetAllLicenciasQuery, List<LicenciaDto>>
{
    private readonly ILicenciaRepository _licenciaRepository;

    private readonly IMapper _mapper;

    public GetAllLicenciasQueryHandler(
        ILicenciaRepository licenciaRepository,
        IMapper mapper
    )
    {
        _licenciaRepository = licenciaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<LicenciaDto>>> Handle(GetAllLicenciasQuery request, CancellationToken cancellationToken)
    {
        var licencias = await _licenciaRepository.GetAll();

        var licenciaDto = _mapper.Map<List<LicenciaDto>>(licencias);

        return licenciaDto!;

    }
}