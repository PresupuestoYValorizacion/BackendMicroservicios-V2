using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Sgo.Presupuestos.GetByIdTitulo;


internal sealed class GetByIdTituloQueryHandler : IQueryHandler<GetByIdTituloQuery, TituloTenantDto>
{
    private readonly ITituloTenantRepository _tituloRepository;

    private readonly IMapper _mapper;

    public GetByIdTituloQueryHandler(
        ITituloTenantRepository tituloRepository,
        IMapper mapper
    )
    {
        _tituloRepository = tituloRepository;
        _mapper = mapper;
    }

    public async Task<Result<TituloTenantDto>> Handle(GetByIdTituloQuery request, CancellationToken cancellationToken)
    {

        var titulo = await _tituloRepository.GetByIdAsync(new TituloTenantId(Guid.Parse(request.Id)), cancellationToken);

        if (titulo is null)
        {
            return Result.Failure<TituloTenantDto>(TituloTenantErrors.NotFound)!;
        }

        var tituloDto = _mapper.Map<TituloTenantDto>(titulo);

        return tituloDto!;


    }
}