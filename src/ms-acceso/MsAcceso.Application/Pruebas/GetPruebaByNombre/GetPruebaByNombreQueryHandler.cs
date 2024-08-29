using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Users;


namespace MsAcceso.Application.Pruebas.GetAllPruebas;


internal sealed class GetPruebaByNombreQueryHandler : IQueryHandler<GetPruebaByNombreQuery, LicenciaDto>
{
    private readonly ILicenciaRepository _licenciaRepository;

    private readonly IMapper _mapper;

    public GetPruebaByNombreQueryHandler(
        ILicenciaRepository licenciaRepository,
        IMapper mapper
    )
    {
        _licenciaRepository = licenciaRepository;
        _mapper = mapper;
    }

    public async Task<Result<LicenciaDto>> Handle(GetPruebaByNombreQuery request, CancellationToken cancellationToken)
    {
        var licencia = await _licenciaRepository.GetByNombre(request.Nombre!, cancellationToken);
        if(licencia == null){
            return Result.Failure<LicenciaDto>(LicenciaErrors.NotFound)!;
        }
        var licenciaDto = _mapper.Map<LicenciaDto>(licencia);

        return licenciaDto!;

    }
}