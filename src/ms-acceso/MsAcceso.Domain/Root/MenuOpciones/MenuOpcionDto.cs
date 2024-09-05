using MsAcceso.Domain.Root.Opciones;

namespace MsAcceso.Domain.Root.MenuOpciones;

public class MenuOpcionDto
{
    public string? Id {get; set;}
    public string? OpcionId {get; set;}
    public string? SistemaId {get; set;}
    public bool? Completed {get; set;}
    public OpcionDto? Opcion {get; set;}
}