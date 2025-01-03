
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Root.Users;

namespace MsAcceso.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    UserId Id, 
    string? Email, 
    string? Username,
    bool IsAdmin,
    LicenciaId LicenciaId,
    RolId RolId
) : ICommand<Guid>;