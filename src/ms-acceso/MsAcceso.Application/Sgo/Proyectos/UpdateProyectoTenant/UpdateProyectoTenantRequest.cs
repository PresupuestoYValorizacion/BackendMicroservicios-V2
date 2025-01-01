namespace MsAcceso.Application.Sgo.Proyectos.UpdateProyectoTenant;

public record UpdateProyectoTenantRequest(
    string Id,
    string Nombre,
    bool IsProyecto

);