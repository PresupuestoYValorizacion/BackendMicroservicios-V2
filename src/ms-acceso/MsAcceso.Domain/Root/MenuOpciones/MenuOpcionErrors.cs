using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.MenuOpciones;

public class MenuOpcionErrors
{
    public static Error MenuOpcionNotFound = new Error(400,"Esta opcion de menú no existe.");
    public static Error MenuOpcionExists = new Error(400,"Esta opcion de menú ya existe, ingrese una diferente.");
}