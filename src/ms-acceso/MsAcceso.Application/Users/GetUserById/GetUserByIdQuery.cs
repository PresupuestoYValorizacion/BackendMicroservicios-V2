using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Parametros.GetUserById;

public sealed record GetUserByIdQuery : IQuery<UserDto>
{
    public Guid Id { get; set; }
}