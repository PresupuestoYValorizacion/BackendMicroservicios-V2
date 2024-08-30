using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Roles.GetRolById;

public sealed record GetRolByIdQuery : IQuery<RolDto>
{
    public Guid RolId {get; set;}
}