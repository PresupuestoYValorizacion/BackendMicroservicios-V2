using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Reports;
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
            var result = _generateReportPdfService.GenerateHojaPresupuestoPdf();

            byte[] reportePdf = result.GeneratePdf();

            return Result.Success(reportePdf, Message.Create)!;
        }
    }
}
