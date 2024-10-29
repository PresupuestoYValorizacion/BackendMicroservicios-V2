
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.Users;
using MsAcceso.Domain.Tenant.UsersTenant;
namespace MsAcceso.Application.Tenant.Users.GetUserByIdTenant;

internal sealed class GetUserByIdTenantQueryHandler : IQueryHandler<GetUserByIdTenantQuery, UserTenantDto?>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IParametroRepository _parametroRepository;
    private readonly IMapper _mapper;
    public GetUserByIdTenantQueryHandler(IUserTenantRepository userTenantRepository,IParametroRepository parametroRepository, IMapper mapper)
    {
        _userTenantRepository = userTenantRepository;
        _parametroRepository = parametroRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserTenantDto?>> Handle(GetUserByIdTenantQuery request, CancellationToken cancellationToken)
    {

        var user = await _userTenantRepository.GetByIdUserIncludes(new UserTenantId(request.Id), cancellationToken);
        

        
        var tipoPersonas = await _parametroRepository.GetRelatedEntitiesAsync( ParametroEnum.TipoPersona, cancellationToken);

        // foreach(var user in resultPagination.Results)
        // {  

            user!.Persona!.Tipo = tipoPersonas.FirstOrDefault(x => x.Id == new ParametroId(user.Persona.TipoId ?? 0));
            user.Persona!.TipoDocumento = await _parametroRepository.GetByIdAsync(new ParametroId(user.Persona.TipoDocumentoId ?? 0), cancellationToken);

        // }


        var userTenantDto = _mapper.Map<UserTenantDto>(user);

        return userTenantDto;

    }
}