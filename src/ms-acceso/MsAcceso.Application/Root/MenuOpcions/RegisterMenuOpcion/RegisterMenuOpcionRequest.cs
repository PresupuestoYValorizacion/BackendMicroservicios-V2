namespace MsAcceso.Application.Root.MenuOpcions.RegisterMenuOpcion;

public record RegisterMenuOpcionRequest(
    string SistemaId,
    string OpcionId,
    bool TieneUrl,
    string Url
);