

using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Username, 
    string Password,
    string EmpresaId 
    ) : ICommand<Guid>;