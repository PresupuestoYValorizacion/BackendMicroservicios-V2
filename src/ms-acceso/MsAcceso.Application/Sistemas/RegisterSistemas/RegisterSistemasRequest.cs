namespace MsAcceso.Application.Sistemas.RegisterSistemas;

public record RegisterSistemasRequest(
    string Nombre,
    string Logo,
    string Url,
    string? Dependecia,
    int Nivel
);