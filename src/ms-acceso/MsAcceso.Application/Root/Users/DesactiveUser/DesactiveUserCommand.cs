using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Root.Users.DesactiveUser;

public sealed record DesactiveUserCommand(
    UserId Id
) : ICommand<Guid>;