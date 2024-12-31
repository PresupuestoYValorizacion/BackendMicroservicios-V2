using QuestPDF.Fluent;

namespace MsAcceso.Domain.Root.Reports
{
    public interface IGenerateReportPdfService
    {
        Document GenerateHojaPresupuestoPdf();
    }
}
