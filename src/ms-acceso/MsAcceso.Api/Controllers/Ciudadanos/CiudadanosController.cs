using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Application.Ciudadanos.GetAllCiudadanos;
using MsAcceso.Domain.Root.Ciudadanos;

namespace MsAcceso.Api.Controllers.Parametros;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/ciudadanos")]
public class CiudadanosController : Controller
{

    private readonly ISender _sender;

    public CiudadanosController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<List<Ciudadano>>> GetAllCiudadanos()
    {
        var request = new GetAllCiudadanosQuery {};
        var results = await _sender.Send(request);

        return Ok(results);
    }
}