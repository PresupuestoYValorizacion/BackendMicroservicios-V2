using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Roles.GetAllSistemasByRolTenant;

internal sealed class GetAllSistemasByRolTenantQueryHandler : IQueryHandler<GetAllSistemasByRolTenantQuery, List<SistemaByRolDto>>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IRolTenantRepository _rolTenantRepository;
    private readonly IRolPermisoTenantRepository _rolPermisoTenantRepository;
    // private readonly IRolPermisoOpcionTenantRepository _rolPermisoOpcionTenantRepository;

    private readonly IMapper _mapper;

    public GetAllSistemasByRolTenantQueryHandler(
        ISistemaRepository sistemaRepository,
        IRolTenantRepository rolTenantRepository,
        IRolPermisoTenantRepository rolPermisoTenantRepository,
        // IRolPermisoOpcionTenantRepository rolPermisoOpcionTenantRepository,

        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _rolTenantRepository = rolTenantRepository;
        _rolPermisoTenantRepository = rolPermisoTenantRepository;
        // _rolPermisoOpcionTenantRepository = rolPermisoOpcionTenantRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemaByRolDto>>> Handle(GetAllSistemasByRolTenantQuery request, CancellationToken cancellationToken)
    {
        var userRolId = new RolId(Guid.Parse(request.UserRolId!));

        var rolId = new RolTenantId(Guid.Parse(request.RolId!));

        List<Sistema> sistemas = await _sistemaRepository.GetAllSistemasByUserRol(userRolId!, cancellationToken);     

        var sistemasDto = _mapper.Map<List<SistemaByRolDto>>(sistemas);

        foreach( var sistema in sistemasDto)
        {
            await ProcessSistemaRecursive(sistema, rolId, cancellationToken);
        }

        return sistemasDto!;

    }

    async Task ProcessSistemaRecursive(SistemaByRolDto sistema, RolTenantId rolId, CancellationToken cancellationToken)
    {
        // Cambiar a este metodo que devuelva un ROlpermiso entidad
        var rolPermiso =  await _rolPermisoTenantRepository.ValidarPermisoMenu(sistema.Id!, rolId, cancellationToken);
        
       sistema.Completed  =  rolPermiso != null;
       
        // foreach(var menuOpcion in sistema.MenuOpciones!)
        // {
        //     // menuOpcion.Completed = await _rolPermisoOpcionTenantRepository.ValidarPermisoOpcion(rolPermiso.id, menuOpcion.OpcionId, cancellationToken);
        // }
        // Si tiene childrens, aplicar la función recursiva
        if (sistema.Childrens != null && sistema.Childrens.Count > 0)
        {
            foreach (var child in sistema.Childrens)
            {
                await ProcessSistemaRecursive(child, rolId, cancellationToken);
            }
        }
    }

}