namespace MsAcceso.Application.Opciones.UpdateOpciones;

public record UpdateOpcionRequest(
    Guid Id,
    string Nombre,
    string Icono,
    string Tooltip
);