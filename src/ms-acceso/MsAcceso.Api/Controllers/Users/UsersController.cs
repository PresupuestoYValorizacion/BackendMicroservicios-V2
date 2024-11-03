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
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Tenant.Users.RegisterUsersTenant;
using MsAcceso.Domain.Tenant.RolsTenant;
using MsAcceso.Application.Tenant.Users.GetUserByIdTenant;
using MsAcceso.Application.Tenant.Users.LoginTenant;
using MsAcceso.Application.Tenant.Users.GetMenusByUserTenant;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Application.Tenant.Users.SingInByTokenTenant;
using MsAcceso.Application.Tenant.Users.UpdateUsersTenant;
using MsAcceso.Domain.Tenant.UsersTenant;
using MsAcceso.Domain.Tenant.PersonasTenant;
using MsAcceso.Application.Tenant.Users.DesactiveUserTenant;
using MsAcceso.Application.Tenant.Users.DeleteUserTenant;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Application.Tenant.Users.GetOpcionesSGATenant;
using MsAcceso.Application.Tenant.Users.ValidarAccesoMenuTenant;
using MsAcceso.Application.Root.Users.LogoutUser;

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
        var token = _httpContextAccessor.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();


        if (userEmail is null)
        {
            return BadRequest("Header no existe");
        }

        bool isTenant = false;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsTenant", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isTenant))
            {
                isTenant = false;
            }
        }

        object command;

        if (isTenant)
        {
            var rol = _httpContextAccessor.HttpContext!.Request.Headers["Rol"].ToString();
            var tenant = _httpContextAccessor.HttpContext!.Request.Headers["Tenant"].ToString();

            command = new SingInByTokenTenantCommand(Email: userEmail!, Token: token!, RolId: rol, TenantId: tenant);
        }
        else
        {
            command = new SingInByTokenCommand(Email: userEmail!, Token: token!);

        }

        var result = await _sender.Send(command, cancellationToken);


        // if (result.IsFailure)
        // {
        //     return Unauthorized(result);
        // }

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
        var rol = _httpContextAccessor.HttpContext!.Request.Headers["Rol"].ToString();

        if (rol is null)
        {
            return BadRequest("Header no existe");
        }

        bool isTenant = false;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsTenant", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isTenant))
            {
                isTenant = false;
            }
        }
        IQuery<List<SistemaByRolDto>> query;

        if (isTenant)
        {
            var userTenantRolId = _httpContextAccessor.HttpContext!.Request.Headers["UserTenantRolId"].ToString();

            query = new GetMenusByUserTenantQuery
            {
                RolId = rol,
                UserTenantRolId = userTenantRolId,
                Dependencia = string.IsNullOrEmpty(dependencia) ? null : dependencia
            };
        }
        else
        {
            query = new GetMenusByUserQuery
            {
                RolId = new RolId(new Guid(rol)),
                Dependencia = string.IsNullOrEmpty(dependencia) ? null : dependencia
            };
        }


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
        bool isTenant = false;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsTenant", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isTenant))
            {
                isTenant = false;
            }
        }
        var rol = _httpContextAccessor.HttpContext!.Request.Headers["Rol"].ToString();

        if (rol is null)
        {
            return BadRequest("Header no existe");
        }

        IQuery<List<MenuOpcionDto>> query;

        if (isTenant)
        {
            var userTenantRolId = _httpContextAccessor.HttpContext!.Request.Headers["UserTenantRolId"].ToString();

            query = new GetOpcionesSGATenantQuery
            {
                UserRolId = new RolId(new Guid(rol)),
                RolId = new RolTenantId(new Guid(userTenantRolId)),
                Url = url
            };

        }
        else
        {

            query = new GetOpcionesSGAQuery
            {
                RolId = new RolId(new Guid(rol)),
                Url = url
            };
        }


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
        var rol = _httpContextAccessor.HttpContext!.Request.Headers["Rol"].ToString();

        if (rol is null)
        {
            return BadRequest("Header no existe");
        }

        bool isTenant = false;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsTenant", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isTenant))
            {
                isTenant = false;
            }
        }

        IQuery<bool> query;

        if (isTenant)
        {

            var userTenantRolId = _httpContextAccessor.HttpContext!.Request.Headers["UserTenantRolId"].ToString();

            query = new ValidarAccesoMenuTenantQuery
            {
                RolId = new RolTenantId(new Guid(userTenantRolId)),
                UserRolId = new RolId(new Guid(rol)),
                Url = url
            };
        }
        else
        {
            query = new ValidarAccesoMenuQuery
            {
                RolId = new RolId(new Guid(rol)),
                Url = url
            };
        }


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
        var command = new LoginCommand(Email: request.Email, Password: request.Password, IsForcedSession: request.IsForcedSession);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }

    [AllowAnonymous]
    [HttpPost("loginTenant")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> LoginTenant(
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken
    )
    {
        var rolId = _httpContextAccessor.HttpContext!.Request.Headers["Rol"].ToString();
        var userTenantId = _httpContextAccessor.HttpContext!.Request.Headers["Tenant"].ToString();

        var command = new LoginTenantCommand(Email: request.Email, Password: request.Password, IsForcedSession: request.IsForcedSession, UserTenantRolId: rolId, UserTenantId: userTenantId);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);

    }


    [AllowAnonymous]
    [HttpPost("logout")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> Logout(
        CancellationToken cancellationToken
    )
    {
        bool isTenant = false;

        if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue("IsTenant", out var isAdminValue))
        {
            if (!bool.TryParse(isAdminValue, out isTenant))
            {
                isTenant = false;
            }
        }
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["UserId"].ToString();
        var tenantId = _httpContextAccessor.HttpContext!.Request.Headers["Tenant"].ToString();

        var command = new LogoutUserCommand(UserId: userId, IsTenant: isTenant, IdTenant: tenantId);

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
            command = new RegisterUserCommand(
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
                        new LicenciaId(request.LicenciaId!.Length > 0 ? new Guid(request.LicenciaId!) : Guid.Empty),
                        new RolId(request.RolId!.Length > 0 ? new Guid(request.RolId!) : Guid.Empty)
                    );
        }
        else
        {
            command = new RegisterUsersTenantCommand(
            request.Email,
            request.Username,
            request.Password,
            request.TipoId,
            request.TipoDocumentoId,
            request.NumeroDocumento,
            request.RazonSocial,
            request.NombreCompleto,
            new RolTenantId(request.RolId!.Length > 0 ? new Guid(request.RolId!) : Guid.Empty)
        );
        }


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
            command = new UpdateUserCommand(
                new UserId(request.Id),
                request.Email,
                request.Username,
                request.IsAdmin,
                new ParametroId(request.PeriodoLicenciaId),
                new LicenciaId(request.LicenciaId!.Length > 0 ? new Guid(request.LicenciaId!) : Guid.Empty),
                new RolId(request.RolId!.Length > 0 ? new Guid(request.RolId!) : Guid.Empty)
            );
        }
        else
        {
            command = new UpdateUsersTenantCommand(
                new UserTenantId(request.Id),
                request.Email,
                request.Username,
                new RolTenantId(request.RolId!.Length > 0 ? new Guid(request.RolId!) : Guid.Empty)
            );
        }

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
            command = new DesactiveUserCommand(
                        new UserId(request.Id)
                    );
        }

        else
        {
            command = new DesactiveUserTenantCommand(
                                    new UserTenantId(request.Id)
                                );
        }


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
            command = new DeleteUserCommand(
                        new UserId(id)
                    );
        }
        else
        {
            command = new DeleteUserTenantCommand(
                                    new UserTenantId(id)
                                );
        }

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
            query = new GetUsersByPaginationQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, OrderAsc = request.OrderAsc, Search = request.Search, OrderBy = request.OrderBy };

        }
        else
        {
            query = new GetUsersByPaginationTenantQuery { PageNumber = request.PageNumber, PageSize = request.PageSize, OrderAsc = request.OrderAsc, Search = request.Search, OrderBy = request.OrderBy };

        }

        var resultados = await _sender.Send(query);

        return Ok(resultados);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<PaginationResult<UserDto>>> GetUserById(Guid id)
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
            query = new GetUserByIdQuery { Id = id };

        }
        else
        {
            query = new GetUserByIdTenantQuery { Id = id };

        }

        var results = await _sender.Send(query);

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
            command = new UpdatePersonaCommand(
            new PersonaId(request.Id),
            new ParametroId(request.TipoId),
            new ParametroId(request.TipoDocumentoId),
            request.NumeroDocumento,
            request.RazonSocial,
            request.NombreCompleto
        );
        }
        else
        {
            command = new UpdatePersonasTenantCommand(
            new PersonaTenantId(request.Id),
            request.TipoId,
            request.TipoDocumentoId,
            request.NumeroDocumento,
            request.RazonSocial,
            request.NombreCompleto
        );
        }

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

}