using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Utils;
using MsAcceso.Application.Licencias.GetAllLicencias;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Tenant.Presupuestos;
using MsAcceso.Application.Presupuestos.GetAllPresupuestos;

namespace MsAcceso.Api.Controllers;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/presupuestos")]
public class PruebaController : Controller
{

    private readonly ISender _sender;

    public PruebaController(ISender sender)
    {
        _sender = sender;
    }


    [AllowAnonymous]
    [ApiVersion(ApiVersions.V1)]
    [HttpGet("get-all")]
    public async Task<ActionResult<Presupuesto>> GetAllPresupuestos()
    {
        var request = new GetAllPresupuestosQuery {};
        var results = await _sender.Send(request);

        return Ok(results);
    }
}