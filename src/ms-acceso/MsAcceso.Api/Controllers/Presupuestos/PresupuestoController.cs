using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sgo.Presupuestos.CreatePartidaTenant;
using MsAcceso.Application.Sgo.Presupuestos.CreateTituloTenant;
using MsAcceso.Application.Sgo.Presupuestos.DeleteTituloPartidaTenant;
using MsAcceso.Application.Sgo.Presupuestos.GetByIdPartida;
using MsAcceso.Application.Sgo.Presupuestos.GetByIdTitulo;
using MsAcceso.Application.Sgo.Presupuestos.GetTitulosByEspecialidad;
using MsAcceso.Application.Sgo.Presupuestos.UpdatePartidaTenant;
using MsAcceso.Application.Sgo.Presupuestos.UpdateTituloTenant;
using MsAcceso.Application.Sgo.Proyectos.GetByIdEspecialidad;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.TitulosTenant;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Presupuestos;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/presupuestos")]
public class PresupuestoController : Controller
{

    private readonly ISender _sender;

    public PresupuestoController(ISender sender)
    {
        _sender = sender;
    }

    


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register-titulo")]
    public async Task<IActionResult> RegisterTitulo(
        [FromBody] CreateTituloTenantCommand command,
        CancellationToken cancellationToken
    )
    {


        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);


    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register-partida")]
    public async Task<IActionResult> RegisterPartida(
        [FromBody] CreatePartidaTenantCommand command,
        CancellationToken cancellationToken
    )
    {

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);


    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("update-partida")]
    public async Task<IActionResult> UpdatePartida(
        [FromBody] UpdatePartidaTenantCommand command,
        CancellationToken cancellationToken
    )
    {

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);


    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPut("update-titulo")]
    public async Task<IActionResult> UpdateTitulo(
        [FromBody] UpdateTituloTenantCommand command,
        CancellationToken cancellationToken
    )
    {

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);


    }

    [HttpPatch("delete")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeleteParametro(
        [FromBody] DeleteTituloPartidaTenantCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all-by-especialidad/{idEspecialidad}")]
    public async Task<ActionResult<TituloTenantDto>> GetAllByEspecialidad(string idEspecialidad)
    {
        var request = new GetTitulosByEspecialidadQuery { EspecialidadId = idEspecialidad };
        var results = await _sender.Send(request);

        return Ok(results);
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id-titulo/{id}")]
    public async Task<ActionResult<TituloTenantDto>> GetByIdTitulo(string id)
    {
        var request = new GetByIdTituloQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id-partida/{id}")]
    public async Task<ActionResult<PartidaTenantDto>> GetByIdPartida(string id)
    {
        var request = new GetByIdPartidaQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-recursos-by-id-partida/{id}")]
    public async Task<ActionResult<ClienteDto>> GetRecursosByIdPartida(string id)
    {
        var request = new GetByIdPartidaQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }
}