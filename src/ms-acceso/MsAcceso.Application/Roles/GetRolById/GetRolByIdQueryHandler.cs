using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.GetRolById;

internal sealed class GetRolByIdQueryHandler : IQueryHandler<GetRolByIdQuery, RolDto>
{
    private readonly IRolRepository _rolRepository;
    private readonly IMapper _mapper;

    public GetRolByIdQueryHandler(
        IRolRepository rolRepository,
        IMapper mapper
    )
    {
        _rolRepository = rolRepository;
        _mapper = mapper;
    }


    public async Task<Result<RolDto>> Handle(GetRolByIdQuery request, CancellationToken cancellationToken)
    {

        var rolExists = await _rolRepository.GetByIdAsync(new RolId(request.RolId), cancellationToken);

        if(rolExists is null)
        {
            return Result.Failure<RolDto>(RolErrors.RolNotExists)!;
        }

        var rolDto = _mapper.Map<RolDto>(rolExists);

        return rolDto!;
    }
}