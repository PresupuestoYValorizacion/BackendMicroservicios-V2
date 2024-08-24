

using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Username, 
    string Password,
    ParametroId TipoId,
    ParametroId TipoDocumentoId,
    string NumeroDocumento,
    string RazonSocial,
    string NombreCompleto
    ) : ICommand<Guid>;