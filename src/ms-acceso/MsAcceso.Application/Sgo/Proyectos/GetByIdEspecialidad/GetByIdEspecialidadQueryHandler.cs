using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Application.Sgo.Proyectos.GetByIdEspecialidad;


internal sealed class GetByIdEspecialidadQueryHandler : IQueryHandler<GetByIdEspecialidadQuery, EspecialidadTenantDto>
{
    private readonly IEspecialidadTenantRepository _especialidadRepository;

    private readonly IMapper _mapper;

    public GetByIdEspecialidadQueryHandler(
        IEspecialidadTenantRepository especialidadRepository,
        IMapper mapper
    )
    {
        _especialidadRepository = especialidadRepository;
        _mapper = mapper;
    }

    public async Task<Result<EspecialidadTenantDto>> Handle(GetByIdEspecialidadQuery request, CancellationToken cancellationToken)
    {

        var especialidad = await _especialidadRepository.GetByIdAsync(new EspecialidadTenantId(Guid.Parse(request.Id)), cancellationToken);

        if (especialidad is null)
        {
            return Result.Failure<EspecialidadTenantDto>(EspecialidadTenantErrors.NotFound)!;
        }

        var especialidadDto = _mapper.Map<EspecialidadTenantDto>(especialidad);

        return especialidadDto!;


    }
}