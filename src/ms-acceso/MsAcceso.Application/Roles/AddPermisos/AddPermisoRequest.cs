
namespace MsAcceso.Application.Roles.AddPermisos;

public record AddPermisoRequest(
    string RolId,
    List<SistemaRequest> SistemasRequest
);

public class SistemaRequest
{
    public Guid Id { get; set; }
    public string? Nombre { get; set; }
    public string? Logo { get; set; }
    public int Nivel { get; set; }
    public string? Url { get; set; }
    public bool Completed { get; set; }
    public List<SistemaRequest> Childrens { get; set; } = new List<SistemaRequest>();
    public List<MenuOpcionRequest> MenuOpciones { get; set; } = new List<MenuOpcionRequest>();
}

public class MenuOpcionRequest
{
    public Guid Id { get; set; }
    public Guid OpcionId { get; set; }
    public Guid SistemaId { get; set; }
    public bool Completed { get; set; }
    public Opcion? Opcion { get; set; }
}

public class Opcion
{
    public Guid Id { get; set; }
    public string? Nombre { get; set; }
    public string? Logo { get; set; }
    public string? Abreviatura { get; set; }
}