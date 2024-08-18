
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.DeleteUser;

public sealed record DeleteUserCommand(
    UserId Id
) : ICommand<Guid>;