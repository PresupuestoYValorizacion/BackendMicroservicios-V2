using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Application.Root.Opciones.GetByIdOpcion;

public sealed record GetByIdOpcionQuery : IQuery<OpcionDto>
{
    public Guid Id { get; set; }
}