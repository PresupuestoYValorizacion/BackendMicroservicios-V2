namespace MsAcceso.Application.Parametros.UpdateParametros;

public record UpdateParametrosRequest(
    int Id,
    string? Nombre,
    string? Descripcion,
    string? Abreviatura,
    string? Valor
);