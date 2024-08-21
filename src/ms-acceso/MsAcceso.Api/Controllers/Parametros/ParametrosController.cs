using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Application.Parametros.DesactiveParametros;
using MsAcceso.Application.Parametros.DeleteParametros;
using MsAcceso.Application.Parametros.GetByIdParametro;
using MsAcceso.Application.Parametros.GetParametroByPagination;
using MsAcceso.Application.Parametros.RegisterParametros;
using MsAcceso.Application.Parametros.UpdateParametros;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Application.Parametros.GetSubnivelesById;

namespace MsAcceso.Api.Controllers.Parametros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/parametros")]
public class ParametrosController : Controller
{

    private readonly ISender _sender;

    public ParametrosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateParametro(
        [FromBody] RegisterParametrosRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterParametrosCommand(
            request.Nombre,
            request.Descripcion,
            request.Abreviatura,
            request.Dependencia,
            request.Nivel,
            request.Valor
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("update")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> UpdateParametro(
        [FromBody] UpdateParametrosRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateParametrosCommand(
            new ParametroId(request.Id),
            request.Nombre,
            request.Descripcion,
            request.Abreviatura,
            request.Valor
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPatch("desactive")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeactiveParametro(
        [FromBody] DesactiveParametrosRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new DesactiveParametrosCommand(
            new ParametroId(request.Id)
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeleteParametro(
        int Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteParametrosCommand(
            new ParametroId(Id)
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
    public async Task<ActionResult<PaginationResult<ParametroDto>>> GetPaginationParametros(
        [FromQuery] GetParametroByPaginationQuery request
    )
    {
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<PaginationResult<ParametroDto>>> GetParametroById(int id)
    {
        var request = new GetByIdParametroQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-subniveles-by-id/{id}")]
    public async Task<ActionResult<PaginationResult<ParametroDto>>> GetSubnivelesById(int id)
    {
        var request = new GetSubnivelesByIdQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }
}