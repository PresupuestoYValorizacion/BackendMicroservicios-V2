namespace MsAcceso.Application.MenuOpcions.UpdateMenuOpcion;

public  record UpdateMenuOpcionRequest(
    string MenuOpcionId,
    string OpcionIdNuevo,
    string OpcionIdAntiguo

);