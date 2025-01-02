using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Clientes.GetAllClientes;

public sealed record GetAllClientesQuery : IQuery<List<ClienteDto>>
{

}