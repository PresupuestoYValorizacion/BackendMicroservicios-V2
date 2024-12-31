using MsAcceso.Domain.Root.Reports.HojaDePresupuesto;
using QuestPDF.Fluent;

namespace MsAcceso.Domain.Root.Reports
{
    public interface IGenerateReportPdfService
    {
        Document GenerateHojaPresupuestoPdf(HojaPresupuesto hojaPresupuesto);
    }
}
