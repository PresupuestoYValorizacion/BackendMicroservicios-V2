using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.CreateCarpetaPresupuestal;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.DeleteCarpetaPresupuestal;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.GetByIdCarpetaPresupuestal;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.GetCarpetasPresupuestales;
using MsAcceso.Application.Sgo.CarpetasPresupuestales.UpdateCarpetaPresupuestal;
using MsAcceso.Application.Sgo.Clientes.CreateClienteTenant;
using MsAcceso.Application.Sgo.Clientes.DeleteClienteTenant;
using MsAcceso.Application.Sgo.Clientes.GetByIdClienteTenant;
using MsAcceso.Application.Sgo.Clientes.GetClienteByPagination;
using MsAcceso.Application.Sgo.Clientes.UpdateClienteTenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Clientes;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/carpetas-presupuestales")]
public class CarpetasPresupuestalesController : Controller
{

    private readonly ISender _sender;

    public CarpetasPresupuestalesController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] CreateCarpetaPresupuestalRequest request,
        CancellationToken cancellationToken
    )
    {

        var command = new CreateCarpetaPresupuestalCommand(request.Dependencia, request.Nombre,request.Nivel);

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
    public async Task<ActionResult<PaginationResult<ClienteDto>>> GetAll(
    )
    {
        var query =  new GetCarpetasPresupuestalesQuery {  };

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