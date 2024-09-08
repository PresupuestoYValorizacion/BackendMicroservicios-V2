using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Parametros.ValidarAccesoMenu;

public sealed record ValidarAccesoMenuQuery : IQuery<bool>
{
    public RolId? RolId { get; set; }
    public string? Url { get; set; }
}