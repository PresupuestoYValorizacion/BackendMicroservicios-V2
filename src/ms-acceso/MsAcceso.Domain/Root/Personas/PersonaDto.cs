
namespace MsAcceso.Domain.Root.Personas;
public class PersonaDto
{
    public string? Id { get; set; }
    public string? Tipo { get; set; }
    public string? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; set; }

    public PersonaNaturalDto? PersonaNatural { get; set; }
    public PersonaJuridicaDto? PersonaJuridica { get; set; }

}

public class PersonaNaturalDto
{
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public string? Nombres { get; set; }
}

public class PersonaJuridicaDto
{
    public string? RazonSocial { get; set; }
}