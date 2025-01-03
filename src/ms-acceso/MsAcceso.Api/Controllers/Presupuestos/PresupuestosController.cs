using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sgo.Reportes.GenerateReporteHojaPresupuestoPdf;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.Presupuestos;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[ApiVersion(ApiVersions.V2)]
[Route("api/v{version:apiVersion}/presupuestos")]
public class PresupuestosController : ControllerBase
{
    private readonly ISender _sender;

    public PresupuestosController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("hoja-presupuesto-pdf")]
    public async Task<IActionResult> GenerateHojadePresupuesto(
        [FromBody] GenerateReporteHojaPresupuestoPdfRequest request,
        CancellationToken cancellationToken) 
    {
        var command = new GenerateReporteHojaPresupuestoPdfCommand(
            request.codPresupuesto,
            request.descPresupuesto,
            request.codSubPresupuesto,
            request.descSubPresupuesto,
            request.cliente,
            request.lugar,
            request.fechaCosto,
            request.titulos
        );

        var result = await _sender.Send(command,cancellationToken);

        return Ok(result);
    }

}
