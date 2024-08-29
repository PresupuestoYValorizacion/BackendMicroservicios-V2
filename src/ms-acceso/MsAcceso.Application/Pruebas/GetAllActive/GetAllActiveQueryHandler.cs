using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
//


namespace MsAcceso.Application.Pruebas.GetAllActive;


internal sealed class GetAllActiveQueryHandler : IQueryHandler<GetAllActiveQuery, List<LicenciaDto>>
{
    private readonly ILicenciaRepository _licenciaRepository;

    private readonly IMapper _mapper;

    public GetAllActiveQueryHandler(
        ILicenciaRepository licenciaRepository,
        IMapper mapper
    )
    {
        _licenciaRepository = licenciaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<LicenciaDto>>> Handle(GetAllActiveQuery request, CancellationToken cancellationToken)
    {
        var licencias = await _licenciaRepository.GetAllActive();
        if(licencias == null){
            return Result.Failure<List<LicenciaDto>>(LicenciaErrors.NotFound)!;
        }

        var licenciaDto = _mapper.Map<List<LicenciaDto>>(licencias);

        return licenciaDto!;

    }

}