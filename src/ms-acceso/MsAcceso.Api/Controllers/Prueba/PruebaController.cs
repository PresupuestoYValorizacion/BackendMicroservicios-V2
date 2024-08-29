using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Pruebas.DeleteLicencias;
using MsAcceso.Application.Pruebas.DesactiveLicencias;
using MsAcceso.Application.Pruebas.GetAllActive;
using MsAcceso.Application.Pruebas.GetAllPruebas;
using MsAcceso.Application.Pruebas.RegisterLicencias;
using MsAcceso.Application.Pruebas.UpdateLicencias;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Prueba;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/prueba")]
public class PruebaController : Controller
{

    private readonly ISender _sender;

    public PruebaController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<List<LicenciaDto>>> GetAllLicencias()
    {
        var request = new GetAllPruebasQuery {};
        var results = await _sender.Send(request);

        return Ok(results);
    }
    
    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-nombre/{nombre}")]
    public async Task<ActionResult<LicenciaDto>> GetByNombre(string nombre)
    {
        var request = new GetPruebaByNombreQuery {Nombre = nombre};
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all-active")]
    public async Task<ActionResult<List<LicenciaDto>>> GetAllActive()
    {
        var request = new GetAllActiveQuery {};
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateLicencia(
        [FromBody] RegisterLicenciasRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterLicenciasCommand(
            request.Nombre
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
    public async Task<IActionResult> UpdateLicencia(
        [FromBody] UpdateLicenciasRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateLicenciasCommand(
            new LicenciaId(request.Id.Value),
            request.Nombre
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
    public async Task<IActionResult> DesactivateLicencia(
        [FromBody] DesactivateLicenciasRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new DesactiveLicenciasCommand(
            new LicenciaId(request.Id.Value)
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
    public async Task<IActionResult> DeleteLicencia(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteLicenciasCommand(
            new LicenciaId(id)
        );

        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}