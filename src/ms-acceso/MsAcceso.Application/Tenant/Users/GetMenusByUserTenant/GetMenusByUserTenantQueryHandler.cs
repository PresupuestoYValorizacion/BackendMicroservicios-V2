using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Users.GetMenusByUserTenant;

internal sealed class GetMenusByUserTenantQueryHandler : IQueryHandler<GetMenusByUserTenantQuery, List<SistemaByRolDto>>
{
    private readonly IRolPermisoTenantRepository _rolPermisoRepository;
    // private readonly IRolTenantRepository _rolRepository;
    private readonly ISistemaRepository _sistemaRepository;
    private readonly IRolPermisoOpcionTenantRepository _rolPermisoOpcionTenantRepository;


    private readonly IMapper _mapper;

    public GetMenusByUserTenantQueryHandler(
        ISistemaRepository sistemaRepository,
        IRolPermisoTenantRepository rolPermisoRepository,
        IRolPermisoOpcionTenantRepository rolPermisoOpcionRepository,
        // IRolTenantRepository rolRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _rolPermisoRepository = rolPermisoRepository;
        _rolPermisoOpcionTenantRepository = rolPermisoOpcionRepository;
        // _rolRepository = rolRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<SistemaByRolDto>>> Handle(GetMenusByUserTenantQuery request, CancellationToken cancellationToken)
    {
        SistemaId? dependencia = request.Dependencia != null ? new SistemaId(new Guid(request.Dependencia)) : null;


        var userRolId = new RolTenantId(Guid.Parse(request.UserTenantRolId!));

        var rolId = new RolId(Guid.Parse(request.RolId!));

        List<Sistema> sistemas = await _sistemaRepository.GetAllSistemasByUserRol(rolId, cancellationToken);

        var sistemasDto = _mapper.Map<List<SistemaByRolDto>>(sistemas);

        foreach (var sistema in sistemasDto)
        {
            await ProcessSistemaRecursive(sistema, userRolId, cancellationToken);
        }

        List<SistemaByRolDto> sistemasFiltrados;
        if (dependencia != null)
        {
            sistemasFiltrados = BuscarPorDependenciaYEstado(sistemasDto, dependencia.ToString()!);
        }
        else
        {
            sistemasFiltrados = FiltrarCompletados(sistemasDto);
        }

        return sistemasFiltrados!;
    }


    async Task ProcessSistemaRecursive(SistemaByRolDto sistema, RolTenantId rolId, CancellationToken cancellationToken)
    {
        var rolPermiso = await _rolPermisoRepository.GetByMenuAndRol(sistema.Id!, rolId, cancellationToken);

        sistema.Completed = rolPermiso != null;

        var tieneRolPermiso = rolPermiso != null;

        foreach (var menuOpcion in sistema.MenuOpciones!)
        {
            menuOpcion.Completed = tieneRolPermiso
                ? await _rolPermisoOpcionTenantRepository.ValidarPermisoOpcion(rolPermiso!.Id!, menuOpcion.OpcionId!, cancellationToken)
                : false;
        }

        if (sistema.Childrens != null && sistema.Childrens.Count > 0)
        {
            foreach (var child in sistema.Childrens)
            {
                await ProcessSistemaRecursive(child, rolId, cancellationToken);
            }
        }
    }
    public static List<SistemaByRolDto> FiltrarCompletados(List<SistemaByRolDto> sistemasDto)
    {
        var result = new List<SistemaByRolDto>();

        void FiltrarRecursivo(SistemaByRolDto sistema)
        {
            if (sistema.Completed)
            {
                var sistemaFiltrado = new SistemaByRolDto
                {
                    Id = sistema.Id,
                    Nombre = sistema.Nombre,
                    Dependencia = sistema.Dependencia,
                    Logo = sistema.Logo,
                    Nivel = sistema.Nivel,
                    Url = sistema.Url,
                    Completed = sistema.Completed,
                    MenuOpciones = sistema.MenuOpciones, // Puedes filtrar `MenuOpciones` si también aplicas el filtro aquí
                    Childrens = sistema.Childrens != null ? FiltrarCompletados(sistema.Childrens) : null // Filtrar recursivamente los hijos
                };
                result.Add(sistemaFiltrado);
            }
        }

        foreach (var sistema in sistemasDto)
        {
            FiltrarRecursivo(sistema);
        }

        return result;
    }


    public static List<SistemaByRolDto> BuscarPorDependenciaYEstado(List<SistemaByRolDto> sistemasDto, string dependencia)
    {
        var result = new List<SistemaByRolDto>();

        void BuscarRecursivo(SistemaByRolDto sistema)
        {
            if (sistema.Dependencia == dependencia && sistema.Completed)
            {
                var sistemaFiltrado = new SistemaByRolDto
                {
                    Id = sistema.Id,
                    Nombre = sistema.Nombre,
                    Dependencia = sistema.Dependencia,
                    Logo = sistema.Logo,
                    Nivel = sistema.Nivel,
                    Url = sistema.Url,
                    Completed = sistema.Completed,
                    MenuOpciones = sistema.MenuOpciones, // Puedes filtrar `MenuOpciones` si también lo necesitas
                    Childrens = sistema.Childrens != null ? FiltrarCompletados(sistema.Childrens) : null // Filtrar recursivamente los hijos
                };
                result.Add(sistemaFiltrado);
            }

            if (sistema.Childrens != null)
            {
                foreach (var child in sistema.Childrens)
                {
                    BuscarRecursivo(child);
                }
            }
        }

        foreach (var sistema in sistemasDto)
        {
            BuscarRecursivo(sistema);
        }

        return result;
    }



}