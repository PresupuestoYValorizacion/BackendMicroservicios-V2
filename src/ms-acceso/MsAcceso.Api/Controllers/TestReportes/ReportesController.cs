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

        [HttpGet("hoja-presupuesto-pdf")]
        public async Task<IResult> GenerateHojadePresupuesto(
            //[FromBody] GenerateReporteHojaPresupuestoPdfRequest request,
            CancellationToken cancellationToken) 
        {
            var command = new GenerateReporteHojaPresupuestoPdfCommand();

            var result = await _sender.Send(command,cancellationToken);

            var mimeType = "application/pdf";
            return Results.File(result.Payload!, contentType: mimeType, "Hoja de presupuestos.pf");
            //if (result.IsFailure)
            //{
            //    return BadRequest(result);
            //}
            //return Ok(result);
        }

        // GET: ReportesController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: ReportesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ReportesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: ReportesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: ReportesController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ReportesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ReportesController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ReportesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
