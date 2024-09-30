using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Application.Root.Roles.GetAllSistemasByRol;

internal sealed class GetAllSistemasByRolQueryHandler : IQueryHandler<GetAllSistemasByRolQuery, List<SistemaByRolDto>>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IMapper _mapper;

    public GetAllSistemasByRolQueryHandler(
        ISistemaRepository sistemaRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemaByRolDto>>> Handle(GetAllSistemasByRolQuery request, CancellationToken cancellationToken)
    {
        var sistemas = await _sistemaRepository.GetAllSistemasByRolAndUserRol(request.RolId!,request.UserRolId!,cancellationToken);

        var sistemasDto = _mapper.Map<List<SistemaByRolDto>>(sistemas);

        return sistemasDto!;

    }
}