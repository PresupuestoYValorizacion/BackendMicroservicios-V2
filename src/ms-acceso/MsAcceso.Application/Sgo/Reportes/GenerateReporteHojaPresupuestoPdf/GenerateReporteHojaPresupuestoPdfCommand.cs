using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Sgo.Reportes.GenerateReporteHojaPresupuestoPdf
{
    public sealed record GenerateReporteHojaPresupuestoPdfCommand() : ICommand<byte[]>;
}
