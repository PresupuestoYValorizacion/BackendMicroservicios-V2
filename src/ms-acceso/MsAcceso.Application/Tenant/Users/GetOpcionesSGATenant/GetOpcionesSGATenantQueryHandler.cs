using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
namespace MsAcceso.Application.Tenant.Users.GetOpcionesSGATenant;

internal sealed class GetOpcionesSGATenantQueryHandler : IQueryHandler<GetOpcionesSGATenantQuery, List<MenuOpcionDto>>
{
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IRolPermisoTenantRepository _rolPermisoTenantRepository;
    private readonly IRolPermisoOpcionTenantRepository _rolPermisoOpcionTenantRepository;
    private readonly IMapper _mapper;

    public GetOpcionesSGATenantQueryHandler(
        ISistemaRepository sistemaRepository,
        IRolPermisoTenantRepository rolPermisoTenantRepository,
        IRolPermisoOpcionTenantRepository rolPermisoOpcionTenantRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _rolPermisoTenantRepository = rolPermisoTenantRepository;
        _rolPermisoOpcionTenantRepository = rolPermisoOpcionTenantRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<MenuOpcionDto>>> Handle(GetOpcionesSGATenantQuery request, CancellationToken cancellationToken)
    {
        string url = request.Url!;

        var sistema = await _sistemaRepository.GetByUrlTenantAsync(url,request.UserRolId!, cancellationToken);

        if(sistema is null)
        {
            return Result.Failure<List<MenuOpcionDto>>(SistemaErrors.SistemaNotFound)!;
        }

        var sistemaDto = _mapper.Map<SistemaByRolDto>(sistema);

        await ProcessSistemaRecursive(sistemaDto, request.RolId!, cancellationToken);

        var menuOpcionesCompletadas = sistemaDto.MenuOpciones!.Where(m => m.Completed == true).OrderBy(x => x.Orden).ToList();

        return menuOpcionesCompletadas!;
    }


    async Task ProcessSistemaRecursive(SistemaByRolDto sistema, RolTenantId rolId, CancellationToken cancellationToken)
    {
        var rolPermiso = await _rolPermisoTenantRepository.GetByMenuAndRol(sistema.Id!, rolId, cancellationToken);

        sistema.Completed = rolPermiso != null;

        var tieneRolPermiso = rolPermiso != null;

        foreach (var menuOpcion in sistema.MenuOpciones!)
        {
            menuOpcion.Completed = tieneRolPermiso
                ? await _rolPermisoOpcionTenantRepository.ValidarPermisoOpcion(rolPermiso!.Id!, menuOpcion.OpcionId!, cancellationToken)
                : false;
        }

    }

}