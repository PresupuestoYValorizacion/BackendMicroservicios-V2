namespace MsAcceso.Application.Users.UpdateUser;

public record UpdateUserRequest(Guid Id, 
                                string? Email, 
                                string? Username, 
                                bool IsAdmin,
                                string LicenciaId,
                                string RolId);