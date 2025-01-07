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
using MsAcceso.Application.Sgo.Proyectos.CreatePresupuestoTenant;
using MsAcceso.Application.Sgo.Proyectos.CreateProyectoTenant;
using MsAcceso.Application.Sgo.Proyectos.DeleteEspecialidadTenant;
using MsAcceso.Application.Sgo.Proyectos.DeleteProyectoTenant;
using MsAcceso.Application.Sgo.Proyectos.GetByIdEspecialidad;
using MsAcceso.Application.Sgo.Proyectos.GetByIdProyecto;
using MsAcceso.Application.Sgo.Proyectos.GetProyectosTenant;
using MsAcceso.Application.Sgo.Proyectos.UpdateProyectoTenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
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
    [HttpPost("register-presupuesto")]
    public async Task<IActionResult> RegisterPresupuesto(
        [FromBody] CreatePresupuestoTenantRequest request,
        CancellationToken cancellationToken
    )
    {

        var command = new CreatePresupuestoTenantCommand(
                        request.Codigo, 
                        request.Descripcion, 
                        request.ClienteId,
                        request.DepartamentoId, 
                        request.ProvinciaId, 
                        request.DistritoId, 
                        request.Fecha, 
                        request.Plazodias, 
                        request.JornadaDiariaId, 
                        request.MonedaId, 
                        request.PresupuestoBaseCD, 
                        request.PresupuestoBaseCI, 
                        request.TotalPresupuestoBase, 
                        request.PresupuestoOfertaCD, 
                        request.PresupuestoOfertaCD, 
                        request.TotalPresupuestoOferta, 
                        request.CarpetaPresupuestalId,
                        request.ProyectoId);

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
        [FromBody] UpdateProyectoTenantRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateProyectoTenantCommand(
            request.Id,
            request.Nombre,
            request.IsProyecto
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete-proyecto/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> Delete(
        string Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteProyectoTenantCommand(
            new ProyectoTenantId(Guid.Parse(Id))
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("delete-especialidad/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeleteEspecialidad(
        string Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteEspecialidadTenantCommand(
            new EspecialidadTenantId(Guid.Parse(Id))
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
    [HttpGet("get-by-id-especialidad/{id}")]
    public async Task<ActionResult<ClienteDto>> GetByIdEspecialidad(string id)
    {
        var request = new GetByIdEspecialidadQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id-proyecto/{id}")]
    public async Task<ActionResult<ClienteDto>> GetByIdProyecto(string id)
    {
        var request = new GetByIdProyectoQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }
}