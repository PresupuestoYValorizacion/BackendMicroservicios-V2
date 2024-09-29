namespace MsAcceso.Application.Root.Parametros.RegisterParametros;

public record RegisterParametrosRequest(
    string Nombre,
    string Descripcion,
    string Abreviatura,
    int Dependencia,
    int Nivel,
    string Valor);