using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MsAcceso.Utils;
using Microsoft.AspNetCore.Authorization;
using MsAcceso.Application.Opciones.GetOpcionByPagination;
using MsAcceso.Application.Products.Create;

namespace MsAcceso.Api.Controllers.Empresas;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/producto")]
public class ProductController : ControllerBase
{

    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [HttpPost]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        
        var result = await _sender.Send(command, cancellationToken);

        if(result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);


    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [MapToApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> GetById(
        int Id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetAllProductsQuery{ Id = Id};

        var result = await _sender.Send(query, cancellationToken);

        if(result.IsFailure)
        {
            return Unauthorized(result);
        }

        return Ok(result);


    }
}