using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MsAcceso.Application.Sgo.Reportes.GenerateReporteHojaPresupuestoPdf;
using MsAcceso.Utils;

namespace MsAcceso.Api.Controllers.TestReportes
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [ApiVersion(ApiVersions.V2)]
    [Route("api/v{version:apiVersion}/reportes")]
    public class ReportesController : ControllerBase
    {
        private readonly ISender _sender;

        public ReportesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("hoja-presupuesto-pdf")]
        public async Task<IResult> GenerateHojadePresupuesto(
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
                request.titulos,
                request.costoDirecto
            );

            var result = await _sender.Send(command,cancellationToken);

            var mimeType = "application/pdf";
            return Results.File(result.Payload!, contentType: mimeType, $"{request.descPresupuesto}.pdf");
        }

    }
}
