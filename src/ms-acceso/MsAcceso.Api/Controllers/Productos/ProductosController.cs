using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Application.Productos.GetAllProductos;
using MsAcceso.Application.Productos.RegisterProductos;
using MsAcceso.Application.Productos.UpdateProductos;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Application.Productos.DeleteProductos;
using MsAcceso.Application.Productos.DesactiveProductos;

namespace MsAcceso.Api.Controllers.Parametros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/productos")]
public class ProductosController : Controller
{

    private readonly ISender _sender;

    public ProductosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateProducto(
        [FromBody] RegisterProductoRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterProductoCommand(
            request.Nombre,
            request.Cantidad
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
    public async Task<IActionResult> UpdateProducto(
        [FromBody] UpdateProductoRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new UpdateProductoCommand(
            new ProductoId(request.Id),
            request.Nombre,
            request.Codigo,
            request.Cantidad
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
    public async Task<IActionResult> DesactivateProducto(
        [FromBody] DesactiveProductoRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new DesactiveProductoCommand(
            new ProductoId(request.Id.Value)
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
    public async Task<IActionResult> DeleteProducto(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var command = new DeleteProductoCommand(
            new ProductoId(id)
        );

        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<List<Producto>>> GetAllProductos()
    {
        var request = new GetAllProductosQuery {};
        var results = await _sender.Send(request);

        return Ok(results);
    }
}