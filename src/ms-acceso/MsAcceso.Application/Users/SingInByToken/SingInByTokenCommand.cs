

using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Application.Users.LoginUser;

namespace MsAcceso.Application.Users.SingInByToken;

public record SingInByTokenCommand(string Email) : ICommand<LoginUserResponse?>;