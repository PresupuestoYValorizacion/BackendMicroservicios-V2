
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.DeleteUser;

public sealed record DeleteUserCommand(
    UserId Id
) : ICommand<Guid>;