using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sistemas.DeleteSistemas;
using MsAcceso.Application.Sistemas.DesactiveSistemas;
using MsAcceso.Application.Sistemas.GetSistemas;
using MsAcceso.Application.Sistemas.GetSistemasByDependencia;
using MsAcceso.Application.Sistemas.GetSistemasById;
using MsAcceso.Application.Sistemas.RegisterSistemas;
using MsAcceso.Application.Sistemas.UpdateSistemas;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Sistemas;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/sistemas")]
public class SistemasController : ControllerBase
{

    private readonly ISender _sender;

    public SistemasController(
        ISender sender
    )
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateSistema(
        [FromBody] RegisterSistemasRequest request, 
        CancellationToken cancellationToken
        )
    {
        var command = new RegisterSistemasCommand(
            request.Nombre,
            request.Logo,
            request.Url,
            request.Dependecia,
            request.Nivel
        );

        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("update")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> UpdateSistema(
        [FromBody] UpdateSistemasRequest request, 
        CancellationToken cancellationToken
        )
    {
        var sistemaId = Guid.Parse(request.Id);

        var command = new UpdateSistemasCommand(
            new SistemaId(sistemaId),
            request.Nombre,
            request.Logo,
            request.Url,
            request.Orden,
            request.EsIntercambio
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
    public async Task<IActionResult> DesactiveSistema(
        [FromBody] DesactiveSistemasRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new DesactiveSistemasCommand(request.Id);

        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeleteSistema(
        string id,
        CancellationToken cancellationToken
    )
    {

        var command = new DeleteSistemasCommand(id);

        var result = await _sender.Send(command,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("get-sistemas")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<ActionResult<PaginationResult<SistemaDto>>> GetSistemas(CancellationToken cancellationToken)
    {
        var request = new GetSistemasQuery {};
        var result = await _sender.Send(request,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("get-sistemas-by-dependencia/{dependencia}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<ActionResult<List<SistemaDto>>> GetSistemasByDependencia(string dependencia, CancellationToken cancellationToken)
    {

        var request = new GetSistemasByDependenciaQuery {
            Dependencia = new SistemaId(dependencia == "null"  ? Guid.Empty :  new Guid(dependencia))
        };

        var result = await _sender.Send(request,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("get-sistemas-by-id/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<ActionResult<PaginationResult<SistemaDto>>> GetSistemasById(
        string id,
        CancellationToken cancellationToken)
    {
        var request = new GetSistemasByIdQuery {Id = id};
        var result = await _sender.Send(request,cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

}

    