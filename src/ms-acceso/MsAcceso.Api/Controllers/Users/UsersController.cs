
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Root.Users.RegisterUser;
using MsAcceso.Application.Root.Users.UpdateUser;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Root.Users.GetUsersByPagination;
using MsAcceso.Application.Root.Users.LoginUser;
using MsAcceso.Application.Root.Users.SingInByToken;
using MsAcceso.Application.Root.Users.DesactiveUser;
using MsAcceso.Application.Root.Users.DeleteUser;
using MsAcceso.Utils;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Application.Root.Users.ValidateIdUsuario;
using MsAcceso.Application.Root.Parametros.GetUserById;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Application.Root.Users.UpdatePersona;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Application.Root.Users.GetOpcionesSGA;
using MsAcceso.Application.Root.Users.GetMenusByUser;
using MsAcceso.Application.Root.Users.ValidarAccesoMenu;
using MsAcceso.Domain.Shared.Request;
using MsAcceso.Application.Tenant.Users.GetUsersByPaginationTenant;

namespace MsAcceso.Api.Controllers.Users;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/users")]
public class UsersController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISender _sender;

    public UsersController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
    }

    [AllowAnonymous]
    [HttpPost("sing-in-by-token")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> SingInByToken(
        CancellationToken cancellationToken
    )
    {
        var userEmail = _httpContextAccessor.HttpContext!.Request.Headers["User-Email"].ToString();

        if (userEmail is null)
        {
            return BadRequest("Header no existe");
        }

        var command = new SingInByTokenCommand(userEmail!);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }

    [AllowAnonymous]
    [HttpGet("get-menus")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> GetMenusByUser(
         [FromQuery] string? dependencia,
        CancellationToken cancellationToken
    )
    {
        var rol = _httpContextAccessor.HttpContext!.Request.Headers["User-Rol"].ToString();

        if (rol is null)
        {
            return BadRequest("Header no existe");
        }

        var query = new GetMenusByUserQuery {
            RolId = new RolId(new Guid(rol)),
            Dependencia = string.IsNullOrEmpty(dependencia) ? null : dependencia 
        };

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }

    [AllowAnonymous]
    [HttpGet("get-opciones-sga")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> GetOpcionesSGA(
         [FromQuery] string? url,
        CancellationToken cancellationToken
    )
    {
        var rol = _httpContextAccessor.HttpContext!.Request.Headers["User-Rol"].ToString();

        if (rol is null)
        {
            return BadRequest("Header no existe");
        }

        var query = new GetOpcionesSGAQuery {
            RolId = new RolId(new Guid(rol)),
            Url = url 
        };

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }

    [AllowAnonymous]
    [HttpGet("validar-acceso")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> ValidarAccesoMenu(
         [FromQuery] string? url,
        CancellationToken cancellationToken
    )
    {
        var rol = _httpContextAccessor.HttpContext!.Request.Headers["User-Rol"].ToString();

        if (rol is null)
        {
            return BadRequest("Header no existe");
        }

        var query = new ValidarAccesoMenuQuery {
            RolId = new RolId(new Guid(rol)),
            Url = url
        };

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }
    


    [AllowAnonymous]
    [HttpPost("login")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> LoginV1(
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new LoginCommand(request.Email, request.Password);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.Username,
            request.Password,
            new ParametroId(request.TipoId),
            new ParametroId(request.TipoDocumentoId),
            request.NumeroDocumento,
            request.RazonSocial,
            request.NombreCompleto,
            request.IsAdmin,
            new ParametroId(request.PeriodoLicenciaId),
            new LicenciaId(request.LicenciaId!.Length> 0 ? new Guid(request.LicenciaId!) : Guid.Empty),
            new RolId(request.RolId!.Length> 0 ? new Guid(request.RolId!) : Guid.Empty)
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("validate-user-id/{Id}")]
    public async Task<IActionResult> ValidateUserId(
        Guid Id,
        CancellationToken cancellationToken
    )
    {
        var command = new ValidateIdUsuarioCommand(Id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateUserCommand(
            new UserId(request.Id),
            request.Email,
            request.Username,
            request.IsAdmin,
            new ParametroId(request.PeriodoLicenciaId),
            new LicenciaId(request.LicenciaId!.Length> 0 ? new Guid(request.LicenciaId!) : Guid.Empty),
            new RolId(request.RolId!.Length> 0 ? new Guid(request.RolId!) : Guid.Empty)
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPatch("desactive")]
    public async Task<IActionResult> DeactiveUser(
        [FromBody] DesactiveUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new DesactiveUserCommand(
            new UserId(request.Id)
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteUserCommand(
            new UserId(id)
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-pagination")]
    public async Task<ActionResult<PaginationResult<UserDto>>> GetPaginationUsers(
            [FromQuery] GetByPaginationRequest request
        )
    {
         object query ;

        if(request.IsAdmin)
        {
           query     = new GetUsersByPaginationQuery{ PageNumber= request.PageNumber, PageSize = request.PageSize, OrderAsc = request.OrderAsc, Search= request.Search, OrderBy = request.OrderBy};

        }else
        {
           query     = new GetUsersByPaginationTenantQuery{ PageNumber= request.PageNumber, PageSize = request.PageSize, OrderAsc = request.OrderAsc, Search= request.Search, OrderBy = request.OrderBy};

        }

        var resultados = await _sender.Send(query);

        return Ok(resultados);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<PaginationResult<UserDto>>> GetUserById(Guid id)
    {
        var request = new GetUserByIdQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("update-persona")]
    public async Task<IActionResult> UpdatePersona(
        [FromBody] UpdatePersonaRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdatePersonaCommand(
            new PersonaId(request.Id),
            new ParametroId(request.TipoId),
            new ParametroId(request.TipoDocumentoId),
            request.NumeroDocumento,
            request.RazonSocial,
            request.NombreCompleto
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

}