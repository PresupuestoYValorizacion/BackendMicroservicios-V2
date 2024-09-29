using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Users.LoginUser;

public record LoginCommand(string Email, string Password) : ICommand<LoginUserResponse?>;