using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Application.Roles.GetRolesByTipo;
using MsAcceso.Application.Roles.GetRolesByPagination;
using MsAcceso.Application.Roles.GetRolById;
using MsAcceso.Application.Roles.RegisterRoles;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Application.Roles.UpdateRoles;
using MsAcceso.Application.Roles.DesactiveRoles;
using MsAcceso.Application.Roles.DeleteRoles;

namespace MsAcceso.Api.Controllers.Parametros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/roles")]
public class RolesController : Controller
{

    private readonly ISender _sender;

    public RolesController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-tipo-rol/{id}")]
    public async Task<ActionResult<List<RolDto>>> GetRolesByTipo(int id)
    {
        var request = new GetRolesByTipoQuery { TipoRolId = new ParametroId(id) };
        var results = await _sender.Send(request);

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

        var rolId = Guid.Parse(id);
        var request = new GetRolByIdQuery { RolId = rolId};
        var results = await _sender.Send(request,cancellationToken);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }

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

        var rolId = new RolId(Guid.Parse(id));
        var request = new GetAllSistemasByRolQuery { RolId = rolId};
        var results = await _sender.Send(request,cancellationToken);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-pagination")]
    public async Task<ActionResult<PagedResults<RolDto>>> GetRolesByPagination(
        [FromQuery] GetRolesByPaginationQuery request,
        CancellationToken cancellationToken
    )
    {
        var results = await _sender.Send(request,cancellationToken);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }

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

        var command = new RegisterRolesCommand(
            request.Nombre,
            new ParametroId(request.TipoRolId),
            request.LicenciaId
        );

        var results = await _sender.Send(command,cancellationToken);

        if(results.IsFailure)
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

        var command = new UpdateRolesCommand(
            new RolId(Guid.Parse(request.RolId)),
            request.Nombre,
            new ParametroId(request.TipoRolId),
            new LicenciaId(request.LicenciaId!.Length > 0 ? new Guid(request.LicenciaId) : Guid.Empty)
        );

        var results = await _sender.Send(command,cancellationToken);

        if(results.IsFailure)
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

        var command = new DesactiveRolesCommand(
            new RolId(Guid.Parse(request.RolId))
        );

        var results = await _sender.Send(command,cancellationToken);

        if(results.IsFailure)
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

        var command = new DeleteRolesCommand(
            new RolId(Guid.Parse(id))
        );

        var results = await _sender.Send(command,cancellationToken);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);
    }
}