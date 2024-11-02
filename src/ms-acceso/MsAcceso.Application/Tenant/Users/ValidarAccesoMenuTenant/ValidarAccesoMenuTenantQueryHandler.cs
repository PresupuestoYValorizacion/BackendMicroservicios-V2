using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.RolPermisos;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Domain.Tenant.RolPermisosOpcionesTenant;
using MsAcceso.Domain.Tenant.RolPermisosTenant;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Users.ValidarAccesoMenuTenant;

internal sealed class ValidarAccesoMenuTenantQueryHandler : IQueryHandler<ValidarAccesoMenuTenantQuery, bool>
{
    private readonly IRolPermisoTenantRepository _rolPermisoRepository;
    private readonly IRolPermisoOpcionTenantRepository _rolPermisoOpcionRepository;
    private readonly ISistemaRepository _sistemaRepository;

    private readonly IMapper _mapper;

    public ValidarAccesoMenuTenantQueryHandler(
        ISistemaRepository sistemaRepository,
        IRolPermisoTenantRepository rolPermisoRepository,
        IRolPermisoOpcionTenantRepository rolPermisoOpcionRepository,
        IMapper mapper
    )
    {
        _sistemaRepository = sistemaRepository;
        _rolPermisoRepository = rolPermisoRepository;
        _rolPermisoOpcionRepository = rolPermisoOpcionRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(ValidarAccesoMenuTenantQuery request, CancellationToken cancellationToken)
    {

        string url = request.Url!;

        url = url.Trim('/');

        bool containsHttp = url.Contains("http");

        if (!containsHttp)
        {
            string[] partesUrl = url!.Split('/');

            url = "/" + partesUrl[0];
        }

        var sistema = await _sistemaRepository.GetByUrlAllTenantAsync(url, request.UserRolId!, cancellationToken);

        if (sistema is null)
        {
            return Result.Failure<bool>(SistemaErrors.SistemaNotFound);
        }

        var sistemaDto = _mapper.Map<SistemaByRolDto>(sistema);

        await ProcessSistemaRecursive(sistemaDto, request.RolId!, cancellationToken);

        bool existePermiso = false;

        if (!containsHttp)
        {

            string[] partesUrl = request.Url!.Trim('/').Split('/');
            existePermiso = VerificarPermisoRecursivo(sistemaDto, request.RolId!, partesUrl, 0);

        }
        else
        {
            existePermiso = sistemaDto.Completed;

        }


        return existePermiso;

    }

    private static bool VerificarPermisoRecursivo(SistemaByRolDto sistema, RolTenantId rolId, string[] partesUrl, int nivelActual)
    {
        bool tienePermiso = sistema.Completed;

        if (!tienePermiso)
        {
            return false;
        }

        if (nivelActual == partesUrl.Length - 1)
        {
            return true;
        }

        string siguienteParte = "/" + partesUrl[nivelActual +1];

        var subsistema = sistema.Childrens?.FirstOrDefault(x => x.Url == siguienteParte);
        if (subsistema != null)
        {
            return VerificarPermisoRecursivo(subsistema, rolId, partesUrl, nivelActual + 1);
        }

        var opcion = sistema.MenuOpciones?.FirstOrDefault(x => x.Url == siguienteParte && x.Completed == true);
        if (opcion != null)
        {
            
            return true;
        }

        return false;
    }

    async Task ProcessSistemaRecursive(SistemaByRolDto sistema, RolTenantId rolId, CancellationToken cancellationToken)
    {
        var rolPermiso = await _rolPermisoRepository.GetByMenuAndRol(sistema.Id!, rolId, cancellationToken);

        sistema.Completed = rolPermiso != null;

        var tieneRolPermiso = rolPermiso != null;

        foreach (var menuOpcion in sistema.MenuOpciones!)
        {
            menuOpcion.Completed = tieneRolPermiso
                ? await _rolPermisoOpcionRepository.ValidarPermisoOpcion(rolPermiso!.Id!, menuOpcion.OpcionId!, cancellationToken)
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
}