using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sgo.Clientes.CreateClienteTenant;
using MsAcceso.Application.Sgo.Clientes.DeleteClienteTenant;
using MsAcceso.Application.Sgo.Clientes.GetAllClientes;
using MsAcceso.Application.Sgo.Clientes.GetByIdClienteTenant;
using MsAcceso.Application.Sgo.Clientes.GetClienteByPagination;
using MsAcceso.Application.Sgo.Clientes.UpdateClienteTenant;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Tenant.ClientesTenant;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Clientes;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/clientes")]
public class ClienteController : Controller
{

    private readonly ISender _sender;

    public ClienteController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterRoles(
        [FromBody] CreateClienteTenantCommand request,
        CancellationToken cancellationToken
    )
    {

        var command = new CreateClienteTenantCommand(request.TipoPersonaId, request.TipoDocumentoId,request.TipoClienteId, request.NumeroDocumento, request.Nombre);

        var results = await _sender.Send(command, cancellationToken);

        if (results.IsFailure)
        {
            return BadRequest(results);
        }

        return Ok(results);


    }

    [HttpPut("update")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> UpdateParametro(
        [FromBody] UpdateClienteTenantRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateClienteTenantCommand(
            request.Id,
            request.TipoPersonaId,
            request.TipoDocumentoId,
            request.TipoClienteId,
            request.NumeroDocumento,
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
    public async Task<IActionResult> DeleteParametro(
        string Id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteClienteTenantCommand(
            new ClienteTenantId(Guid.Parse(Id))
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
    public async Task<ActionResult<PaginationResult<ClienteDto>>> GetPaginationClientes(
        [FromQuery] GetClienteByPaginationQuery request
    )
    {
        var results = await _sender.Send(request);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all-clientes")]
    public async Task<ActionResult<PaginationResult<ClienteDto>>> GetAllClientes(
    )
    {
        var query = new GetAllClientesQuery();
        var results = await _sender.Send(query);

        return Ok(results);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-by-id/{id}")]
    public async Task<ActionResult<ClienteDto>> GetClienteById(string id)
    {
        var request = new GetByIdClientTenantQuery { Id = id };
        var results = await _sender.Send(request);

        return Ok(results);
    }
}