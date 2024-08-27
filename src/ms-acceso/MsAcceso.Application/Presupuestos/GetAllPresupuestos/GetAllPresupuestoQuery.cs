using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.Presupuestos;

namespace MsAcceso.Application.Presupuestos.GetAllPresupuestos;

public sealed record GetAllPresupuestosQuery : IQuery<List<Presupuesto>>
{

}