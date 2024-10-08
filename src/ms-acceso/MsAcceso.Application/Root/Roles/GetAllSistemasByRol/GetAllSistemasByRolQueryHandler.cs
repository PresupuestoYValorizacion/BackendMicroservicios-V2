using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
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
        var rolId = new RolId(Guid.Parse(request.RolId!));

        List<Sistema> sistemas = await _sistemaRepository.GetAllSistemasByRol(rolId, cancellationToken);

        foreach(var sistema in sistemas)
        {
            
        }


        var sistemasDto = _mapper.Map<List<SistemaByRolDto>>(sistemas);

        return sistemasDto!;

    }
}