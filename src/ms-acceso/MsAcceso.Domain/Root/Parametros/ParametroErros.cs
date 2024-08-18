using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Parametros;

public static class ParametroErrors
{
    public static Error ParametroExists = new Error(409,"Este parametro ya existe, cree uno nuevo");
    public static Error ParametroNotFound = new Error(404,"Este parametro no existe");
    public static Error DependenciaNotFound = new Error(404,"Este parametro dependencia no existe");
    public static Error ValorExists = new Error(404,"Este valor ya existe en este subnivel");
}