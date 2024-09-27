namespace MsAcceso.Application.Root.MenuOpcions.UpdateMenuOpcion;

public  record UpdateMenuOpcionRequest(
    string MenuOpcionId,
    string OpcionId,
    bool TieneUrl,
    string Url,
    int Orden,
    bool EsIntercambio

);