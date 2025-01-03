using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Reports.HojaDePresupuesto;

namespace MsAcceso.Application.Sgo.Reportes.GenerateReporteHojaPresupuestoPdf;

public sealed record GenerateReporteHojaPresupuestoPdfCommand(
    string codPresupuesto,
    string descPresupuesto,
    string codSubPresupuesto,
    string descSubPresupuesto,
    string cliente, 
    string lugar, 
    string fechaCosto,
    List<Titulo> titulos
) : ICommand<byte[]>;