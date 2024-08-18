
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.DesactiveUser;


public sealed record DesactiveUserCommand(
    UserId Id,
    Guid UsuarioModificacion
    ) : ICommand<Guid>;