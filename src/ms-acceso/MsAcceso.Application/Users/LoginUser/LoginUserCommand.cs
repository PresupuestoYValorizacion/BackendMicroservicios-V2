

using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Users.LoginUser;

public record LoginCommand(string Email, string Password) : ICommand<LoginUserResponse?>;