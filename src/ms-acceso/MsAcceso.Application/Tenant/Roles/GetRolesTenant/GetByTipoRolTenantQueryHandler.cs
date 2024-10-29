using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.RolsTenant;


namespace MsAcceso.Application.Tenant.Roles.GetRolesTenant;


internal sealed class GetRolesTenantQueryHandler : IQueryHandler<GetRolesTenantQuery, List<RolTenantDto>>
{
    private readonly IRolTenantRepository _rolRepository;

    private readonly IMapper _mapper;

    public GetRolesTenantQueryHandler(
        IRolTenantRepository rolRepository,
        IMapper mapper
    )
    {
        _rolRepository = rolRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<RolTenantDto>>> Handle(GetRolesTenantQuery request, CancellationToken cancellationToken)
    {
        var roles = await _rolRepository.GetAllAsync();

        var rolesDto = _mapper.Map<List<RolTenantDto>>(roles);

        return rolesDto!;

    }
}