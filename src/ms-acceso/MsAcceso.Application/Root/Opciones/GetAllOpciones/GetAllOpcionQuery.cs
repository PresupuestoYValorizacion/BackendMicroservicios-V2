using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.Opciones.GetAllOpcionQuery;

public sealed record GetAllOpcionQuery : IQuery<List<OpcionDto>>{


}