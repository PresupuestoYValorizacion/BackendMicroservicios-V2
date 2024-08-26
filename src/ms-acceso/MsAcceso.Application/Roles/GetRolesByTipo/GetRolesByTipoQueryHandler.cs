using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.GetRolesByTipo;

internal sealed class GetRolesByTipoQueryHandler : IQueryHandler<GetRolesByTipoQuery, List<RolDto>>
{
    private readonly IRolRepository _rolRepository;

    private readonly IMapper _mapper;

    public GetRolesByTipoQueryHandler(
        IRolRepository rolRepository,
        IMapper mapper
    )
    {
        _rolRepository = rolRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RolDto>>> Handle(GetRolesByTipoQuery request, CancellationToken cancellationToken)
    {
        var roles = await _rolRepository.GetRolesByTipoAsync(request.TipoRolId!);

        var rolesDto = _mapper.Map<List<RolDto>>(roles);

        return rolesDto!;

    }
}