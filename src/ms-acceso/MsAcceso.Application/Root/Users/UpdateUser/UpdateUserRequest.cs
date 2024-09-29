namespace MsAcceso.Application.Root.Users.UpdateUser;

public record UpdateUserRequest(Guid Id, 
                                string? Email, 
                                string? Username, 
                                bool IsAdmin,
                                int PeriodoLicenciaId,
                                string LicenciaId,
                                string RolId
                               );