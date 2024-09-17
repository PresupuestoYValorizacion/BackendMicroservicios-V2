using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Opciones.DesactiveOpciones;
using MsAcceso.Application.Opciones.DeleteOpciones;
using MsAcceso.Application.Opciones.GetOpcionByPagination;
using MsAcceso.Application.Opciones.RegisterOpciones;
using MsAcceso.Application.Opciones.UpdateOpciones;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Application.Opciones.GetByIdOpcion;
using MsAcceso.Utils;
using MsAcceso.Application.Licencias.GetAllLicencias;
using MsAcceso.Application.Opciones.GetAllOpcionQuery;

namespace MsAcceso.Api.Controllers.Opciones;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/opciones")]
public class OpcionesController : ControllerBase
{

    private readonly ISender _sender;

    public OpcionesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateOpcion(
        [FromBody] RegisterOpcionRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterOpcionCommand(
            request.Nombre,
            request.Icono,
            request.Tooltip
        );

        var result = await _sender.Send(command,cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPut("update")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> UpdateOpcion(
        [FromBody] UpdateOpcionRequest request,
        CancellationToken cancellationToken
    )
    {

        var command = new UpdateOpcionCommand(
            new OpcionId(request.Id),
            request.Nombre,
            request.Icono,
            request.Tooltip
        );

        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPatch("desactive")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> Deactivepcion(
        [FromBody] DesactiveOpcionesRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new DesactiveOpcionesCommand(
            new OpcionId(request.Id)
        );

        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeleteOpcion(
        Guid Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteOpcionCommand(
            new OpcionId(Id)
        );

        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-pagination")]
    public async Task<ActionResult<PaginationResult<OpcionDto>>> GetPaginationOpciones(
        [FromQuery] GetOpcionByPaginationQuery request
    )
    {
        var results = await _sender.Send(request);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }
        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<PaginationResult<OpcionDto>>> GetOpcionesById(Guid id)
    {
        var request = new GetByIdOpcionQuery { Id = id };
        var results = await _sender.Send(request);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }
        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<List<OpcionDto>>> GetAllOpciones(CancellationToken cancellationToken)
    {
        var request = new GetAllOpcionQuery {};

        var results = await _sender.Send(request,cancellationToken);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }
        return Ok(results);
    }
}