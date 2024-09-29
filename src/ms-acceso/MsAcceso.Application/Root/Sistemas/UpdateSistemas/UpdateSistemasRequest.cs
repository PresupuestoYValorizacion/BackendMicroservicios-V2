namespace MsAcceso.Application.Root.Sistemas.UpdateSistemas;

public record UpdateSistemasRequest(
    string Id,
    string? Nombre,
    string? Logo,
    string? Url,
    int Orden,
    bool EsIntercambio
);