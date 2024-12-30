using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.CreateCarpetaPresupuestal;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.DeleteCarpetaPresupuestal;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.GetByIdCarpetaPresupuestal;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.GetCarpetasPresupuestales;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.UpdateCarpetaPresupuestal;
using MsAcceso.Application.Sgo.Proyectos.CreateEspecialidadTenant;
using MsAcceso.Application.Sgo.Proyectos.CreateProyectoTenant;
using MsAcceso.Application.Sgo.Proyectos.GetProyectosTenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Proyectos;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/proyectos")]
public class ProyectoController : Controller
{

    private readonly ISender _sender;

    public ProyectoController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register-proyecto")]
    public async Task<IActionResult> RegisterProyecto(
        [FromBody] CreateProyectoTenantRequest request,
        CancellationToken cancellationToken
    )
    {

        var command = new CreateProyectoTenantCommand(request.Nombre);

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);

    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register-especialidad")]
    public async Task<IActionResult> RegisterEspecialidad(
        [FromBody] CreateEspecialidadTenantRequest request,
        CancellationToken cancellationToken
    )
    {

        var command = new CreateEspecialidadTenantCommand(request.Nombre, request.ProyectoId);

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);

    }

    [HttpPut("update")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateCarpetaPresupuestalRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateCarpetaPresupuestalCommand(
            request.Id,
            request.Nombre
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
    public async Task<IActionResult> Delete(
        string Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteCarpetaPresupuestalCommand(
            new CarpetaPresupuestalTenantId(Guid.Parse(Id))
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
    [HttpGet("get-all")]
    public async Task<ActionResult<PaginationResult<ProyectoTenantDto>>> GetAll(
    )
    {
        var query =  new GetProyectosTenantQuery {  };

        var results = await _sender.Send(query);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<ClienteDto>> GetById(string id)
    {
        var request = new GetByIdCarpetaPresupuestalQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }
}