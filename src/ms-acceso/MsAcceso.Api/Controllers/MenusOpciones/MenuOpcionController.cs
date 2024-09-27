using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Root.MenuOpcions.DeleteMenuOpcion;
using MsAcceso.Application.Root.MenuOpcions.DesactiveMenuOpcions;
using MsAcceso.Application.Root.MenuOpcions.GetMenuOpcionById;
using MsAcceso.Application.Root.MenuOpcions.GetMenuOpcionesBySistema;
using MsAcceso.Application.Root.MenuOpcions.RegisterMenuOpcion;
using MsAcceso.Application.Root.MenuOpcions.UpdateMenuOpcion;
using MsAcceso.Domain.Root.MenuOpciones;
using MsAcceso.Domain.Root.Opciones;
using MsAcceso.Domain.Root.Sistemas;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.MenusOpciones;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/menu-opciones")]
public class MenuOpcionController : ControllerBase
{
    private readonly ISender _sender;

    public MenuOpcionController(
        ISender sender
    )
    {
        _sender = sender;
    }

    [HttpPost("register")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> CreateMenuOpcion(
        [FromBody] RegisterMenuOpcionRequest request,
        CancellationToken cancellationToken
    )
    {
        var menuId = new SistemaId(Guid.Parse(request.SistemaId));
        var opcionId = new OpcionId(Guid.Parse(request.OpcionId));

        var commmand = new RegisterMenuOpcionCommand(
            opcionId,
            menuId,
            request.TieneUrl,
            request.Url
        );

        var result = await _sender.Send(commmand, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("update")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> UpdateMenuOpcion(
        [FromBody] UpdateMenuOpcionRequest request,
        CancellationToken cancellationToken
    )
    {
        var menuOpcionId = new MenuOpcionId(Guid.Parse(request.MenuOpcionId));
        var opcionId= new OpcionId(Guid.Parse(request.OpcionId));

        var commmand = new UpdateMenuOpcionCommand(
            menuOpcionId,
            opcionId,
            request.TieneUrl,
            request.Url,
            request.Orden,
            request.EsIntercambio
        );

        var result = await _sender.Send(commmand, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPatch("desactive")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DesactiveMenuOpcion(
        [FromBody] DesactiveMenuOpcionRequest request,
        CancellationToken cancellationToken
    )
    {
        var menuOpcionId = new MenuOpcionId(Guid.Parse(request.MenuOpcionId));

        var commmand = new DesactiveMenuOpcionCommand(
            menuOpcionId
        );

        var result = await _sender.Send(commmand, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("delete")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<IActionResult> DeleteMenuOpcion(
        [FromBody] DeleteMenuOpcionRequest request,
        CancellationToken cancellationToken
    )
    {
        var menuOpcionId = new MenuOpcionId(Guid.Parse(request.MenuOpcionId));

        var commmand = new DeleteMenuOpcionCommand(
            menuOpcionId
        );

        var result = await _sender.Send(commmand, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("get-by-id/{id}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<ActionResult<MenuOpcionDto>> GetMenuOpcionById(
        string id,
        CancellationToken cancellationToken
    )
    {
        var request = new GetMenuOpcionByIdQuery{ Id = id };

        var result = await _sender.Send(request, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpGet("get-by-menu/{menuId}")]
    [ApiVersion(ApiVersions.V1)]
    public async Task<ActionResult<MenuOpcionDto>> GetMenuOpcionByMenu(
        string menuId,
        CancellationToken cancellationToken
    )
    {
        var request = new GetMenuOpcionesBySistemaQuery(new SistemaId(new Guid(menuId))) ;

        var result = await _sender.Send(request, cancellationToken);

        if(result.IsFailure)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}