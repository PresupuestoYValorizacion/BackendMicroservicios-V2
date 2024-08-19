

using MsAcceso.Application.Abstractions.Messaging;

namespace MsAcceso.Application.Users.ValidateIdUsuario;

public record ValidateIdUsuarioCommand(Guid IdUsuario) : ICommand<bool>;