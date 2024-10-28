using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Application.Root.Roles.GetRolesByTipo;
using MsAcceso.Application.Root.Roles.GetRolesByPagination;
using MsAcceso.Application.Root.Roles.GetRolById;
using MsAcceso.Application.Root.Roles.RegisterRoles;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Application.Root.Roles.UpdateRoles;
using MsAcceso.Application.Root.Roles.DesactiveRoles;
using MsAcceso.Application.Root.Roles.DeleteRoles;
using MsAcceso.Application.Root.Roles.AddPermisos;
using MsAcceso.Application.Tenant.Roles.GetRolesByPaginationTenant;
using MsAcceso.Domain.Shared.Request;
using MsAcceso.Application.Tenant.Roles.GetRolByIdTenant;
using MsAcceso.Application.Tenant.Roles.RegisterRoleTenant;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Tenant.Roles.UpdateRoleTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Application.Tenant.Roles.DesactiveRoleTenant;
using MsAcceso.Application.Tenant.Roles.DeleteRoleTenant;
using MsAcceso.Application.Root.Roles.GetAllSistemasByRol;
using MsAcceso.Application.Tenant.Roles.GetAllSistemasByRolTenant;
using MsAcceso.Application.Tenant.Roles.AddPermisosTenant;
using MsAcceso.Application.Tenant.Roles.GetRolesTenant;

namespace MsAcceso.Api.Controllers.Parametros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/roles")]
public class RolesController : Controller
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISender _sender;

    public RolesController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-tipo-rol/{id}")]
    public async Task<ActionResult<List<RolDto>>> GetRolesByTipo(int id)
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }

        object query;

        if (isAdmin)
        {
            query = new GetRolesByTipoQuery { TipoRolId = new ParametroId(id) };

        }
        else
        {
            query = new GetRolesTenantQuery { };

        }

        var results = await _sender.Send(query);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<List<RolDto>>> GetRolById(
        string id,
        CancellationToken cancellationToken
    )
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }


        var rolId = Guid.Parse(id);

        object query;
        if (isAdmin)
        {
            query = new GetRolByIdQuery { RolId = rolId };

        }
        else
        {
            query = new GetRolByIdTenantQuery { RolId = rolId };

        }


        var results = await _sender.Send(query, cancellationToken);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-sistemas-by-rol/{id}")]
    public async Task<ActionResult<List<RolDto>>> GetSistemasByRol(
        string id,
        CancellationToken cancellationToken
    )
    {
        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }


        object query;
        if (isAdmin)
        {
            query = new GetAllSistemasByRolQuery { RolId = id };

        }
        else
        {
            string userRol = _httpContextAccessor.HttpContext!.Request.Headers["Rol"]!.ToString();

            query = new GetAllSistemasByRolTenantQuery { RolId = id, UserRolId = userRol };

        }

        var results = await _sender.Send(query, cancellationToken);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-pagination")]
    public async Task<ActionResult<PagedResults<RolDto>>> GetRolesByPagination(
        [FromQuery] GetByPaginationRequest request,
        CancellationToken cancellationToken
    )
    {
        bool isAdmin = bool.Parse(_httpContextAccessor.HttpContext!.Request.Headers["IsAdmin"]!);

        object query;

        if (isAdmin)
        {
            query = new GetRolesByPaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, OrderAsc = request.OrderAsc, Search = request.Search, OrderBy = request.OrderBy };
        }
        else
        {
            query = new GetRolesByPaginationTenantQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, OrderAsc = request.OrderAsc, Search = request.Search, OrderBy = request.OrderBy };

        }

        var results = await _sender.Send(query, cancellationToken);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterRoles(
        [FromBody] RegisterRolesRequest request,
        CancellationToken cancellationToken
    )
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }

        ICommand<Guid> command;

        if (isAdmin)
        {
            command = new RegisterRolesCommand(
                        request.Nombre,
                        new ParametroId(request.TipoRolId),
                        request.LicenciaId
                    );
        }
        else
        {
            command = new RegisterRoleTenantCommand(request.Nombre);

        }

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);


    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateRoles(
        [FromBody] UpdateRolesRequest request,
        CancellationToken cancellationToken
    )
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }

        ICommand<Guid> command;

        if (isAdmin)
        {
            command = new UpdateRolesCommand(
                            new RolId(Guid.Parse(request.RolId)),
                            request.Nombre,
                            new ParametroId(request.TipoRolId),
                            new LicenciaId(request.LicenciaId!.Length > 0 ? new Guid(request.LicenciaId) : Guid.Empty)
                        );
        }
        else
        {
            command = new UpdateRoleTenantCommand(new RolTenantId(Guid.Parse(request.RolId)), request.Nombre);

        }

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("add-permisos")]
    public async Task<IActionResult> AddPermisos(
        [FromBody] AddPermisoRequest request,
        CancellationToken cancellationToken
    )
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }

        ICommand<Guid> command;

        if (isAdmin)
        {
            command = new AddPermisosCommand(
                        new RolId(Guid.Parse(request.RolId)),
                        request.SistemasRequest
                    );

        }
        else
        {

            command = new AddPermisosTenantCommand(
                        new RolTenantId(Guid.Parse(request.RolId)),
                        request.SistemasRequest
                    );

        }

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPatch("desactive")]
    public async Task<IActionResult> DesactiveRoles(
        [FromBody] DesactiveRolesRequest request,
        CancellationToken cancellationToken
    )
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }

        ICommand<Guid> command;

        if (isAdmin)
        {
            command = new DesactiveRolesCommand(
                            new RolId(Guid.Parse(request.RolId))
                        );
        }
        else
        {
            command = new DesactiveRoleTenantCommand(new RolTenantId(Guid.Parse(request.RolId)));

        }

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRoles(
        string id,
        CancellationToken cancellationToken
    )
    {

        bool isAdmin = true;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsAdmin", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isAdmin))
            {
                isAdmin = true;
            }
        }

        ICommand<Guid> command;

        if (isAdmin)
        {
            command = new DeleteRolesCommand(
            new RolId(Guid.Parse(id))
        );
        }
        else
        {
            command = new DeleteRoleTenantCommand(new RolTenantId(Guid.Parse(id)));

        }

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);
    }
}