using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.GetAllOpcionQuery;

public sealed record GetAllOpcionQuery : IQuery<List<OpcionDto>>{


}