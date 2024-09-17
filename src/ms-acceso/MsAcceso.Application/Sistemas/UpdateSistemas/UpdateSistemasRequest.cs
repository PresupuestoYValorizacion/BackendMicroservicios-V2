namespace MsAcceso.Application.Sistemas.UpdateSistemas;

public record UpdateSistemasRequest(
    string Id,
    string? Nombre,
    string? Logo,
    string? Url,
    int Orden,
    bool EsIntercambio
);