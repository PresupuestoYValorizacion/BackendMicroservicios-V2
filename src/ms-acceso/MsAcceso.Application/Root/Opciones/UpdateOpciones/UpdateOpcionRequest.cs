namespace MsAcceso.Application.Root.Opciones.UpdateOpciones;

public record UpdateOpcionRequest(
    Guid Id,
    string Nombre,
    string Icono,
    string Tooltip
);