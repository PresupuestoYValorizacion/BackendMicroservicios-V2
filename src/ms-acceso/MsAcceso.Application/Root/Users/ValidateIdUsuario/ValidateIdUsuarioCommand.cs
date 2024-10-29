

using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Root.Users.ValidateIdUsuario;

public record ValidateIdUsuarioCommand(Guid IdUsuario) : ICommand<string>;