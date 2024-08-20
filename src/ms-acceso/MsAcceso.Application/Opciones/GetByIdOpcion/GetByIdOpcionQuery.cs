using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Opciones.GetByIdOpcion;

public sealed record GetByIdOpcionQuery : IQuery<OpcionDto>
{
    public Guid Id { get; set; }
}