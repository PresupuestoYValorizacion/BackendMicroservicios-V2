using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.GetRolByIdTenant;

internal sealed class GetRolByIdQueryHandler : IQueryHandler<GetRolByIdTenantQuery, RolTenantDto>
{
    private readonly IRolTenantRepository _rolRepository;
    private readonly IMapper _mapper;

    public GetRolByIdQueryHandler(
        IRolTenantRepository rolRepository,
        IMapper mapper
    )
    {
        _rolRepository = rolRepository;
        _mapper = mapper;
    }


    public async Task<Result<RolTenantDto>> Handle(GetRolByIdTenantQuery request, CancellationToken cancellationToken)
    {

        var rolExists = await _rolRepository.GetByIdAsync(new RolTenantId(request.RolId), cancellationToken);

        if(rolExists is null)
        {
            return Result.Failure<RolTenantDto>(RolTenantErrors.RolNotExists)!;
        }

        var rolDto = _mapper.Map<RolTenantDto>(rolExists);

        return rolDto!;
    }
}