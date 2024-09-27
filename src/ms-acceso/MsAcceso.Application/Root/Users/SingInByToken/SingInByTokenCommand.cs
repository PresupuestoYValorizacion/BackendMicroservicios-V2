using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Root.Users.LoginUser;

namespace MsAcceso.Application.Root.Users.SingInByToken;

public record SingInByTokenCommand(string Email) : ICommand<LoginUserResponse?>;