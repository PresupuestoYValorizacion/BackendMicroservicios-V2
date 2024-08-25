
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Users.RegisterUser;
using MsAcceso.Application.Users.UpdateUser;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Application.Users.GetUsersByPagination;
using MsAcceso.Application.Users.LoginUser;
using MsAcceso.Application.Users.SingInByToken;
using MsAcceso.Application.Users.DesactiveUser;
using MsAcceso.Application.Users.DeleteUser;
using MsAcceso.Utils;
using MsAcceso.Domain.Root.Users;
using MsAcceso.Application.Users.ValidateIdUsuario;
using MsAcceso.Application.Parametros.GetUserById;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Application.Users.UpdatePersona;
using MsAcceso.Domain.Root.Personas;

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
            request.NombreCompleto
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
            request.Username
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
            new UserId(request.Id),
            request.UsuarioModificacion
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
        Guid Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteUserCommand(
            new UserId(Id)
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
    public async Task<ActionResult<PaginationResult<UserDto>>> GetPaginationVehiculo(
            [FromQuery] GetUsersByPaginationQuery request
        )
    {
        var resultados = await _sender.Send(request);

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