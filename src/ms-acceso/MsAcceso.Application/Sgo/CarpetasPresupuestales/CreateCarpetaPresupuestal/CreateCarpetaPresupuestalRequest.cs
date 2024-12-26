namespace MsAcceso.Application.Sgo.CarpetasPresupuestales.CreateCarpetaPresupuestal;

public record CreateCarpetaPresupuestalRequest(
    string Dependencia,
    string Nombre,
    int Nivel
);