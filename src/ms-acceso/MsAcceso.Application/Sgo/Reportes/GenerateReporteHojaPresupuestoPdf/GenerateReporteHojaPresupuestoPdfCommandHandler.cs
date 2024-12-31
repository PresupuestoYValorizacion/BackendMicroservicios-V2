using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Reports;
using MsAcceso.Domain.Root.Reports.HojaDePresupuesto;
using QuestPDF.Fluent;

namespace MsAcceso.Application.Sgo.Reportes.GenerateReporteHojaPresupuestoPdf
{
    internal class GenerateReporteHojaPresupuestoPdfCommandHandler : ICommandHandler<GenerateReporteHojaPresupuestoPdfCommand, byte[]>
    {
        private readonly IGenerateReportPdfService _generateReportPdfService;

        public GenerateReporteHojaPresupuestoPdfCommandHandler(IGenerateReportPdfService generateReportPdfService)
        {
            _generateReportPdfService = generateReportPdfService;
        }

        public async Task<Result<byte[]>> Handle(GenerateReporteHojaPresupuestoPdfCommand request, CancellationToken cancellationToken)
        {
            var hojaPresupuesto = HojaPresupuesto.Create(
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

            var resultPdf = _generateReportPdfService.GenerateHojaPresupuestoPdf(hojaPresupuesto);

            byte[] reportePdf = resultPdf.GeneratePdf();

            return Result.Success(reportePdf, Message.Create)!;
        }
    }
}
