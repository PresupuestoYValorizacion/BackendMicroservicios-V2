using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Root.Libros.GetLibroByPagination;
using MsAcceso.Application.Root.Libros.RegisterLibros;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Libros;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Libros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/libros")]

public class LibrosController : ControllerBase
{

    private readonly ISender _sender;

    public LibrosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateLibro(
        [FromBody] RegisterLibroRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new RegisterLibroCommand(
            request.Nombre,
            request.Descripcion,
            request.Precio
        );

        var result = await _sender.Send(command,cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-pagination")]
    public async Task<ActionResult<PaginationResult<LibroDto>>> GetPaginationLibros(
        [FromQuery] GetLibroByPaginationQuery request
    )
    {
        var results = await _sender.Send(request);

        if(results.IsFailure)
        {
            return BadRequest(results);
        }
        return Ok(results);
    }

}