using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Users.LogoutUser;

public record LogoutUserCommand(string UserId, bool IsTenant, string IdTenant) : ICommand<LogoutUserResponse>;