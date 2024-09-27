using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Root.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email, 
    string Username, 
    string Password,
    ParametroId TipoId,
    ParametroId TipoDocumentoId,
    string NumeroDocumento,
    string RazonSocial,
    string NombreCompleto,
    bool IsAdmin,
    ParametroId PeriodoLicenciaId,
    LicenciaId LicenciaId,
    RolId RolId
) : ICommand<Guid>;