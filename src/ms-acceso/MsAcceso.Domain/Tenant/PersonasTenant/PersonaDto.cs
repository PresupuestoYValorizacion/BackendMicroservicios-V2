
namespace MsAcceso.Domain.Root.Personas;
public class PersonaTenantDto
{
    public string? Id { get; set; }
    public string? Tipo { get; set; }
    public string? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? TipoDocumentoId { get; set; }
    public string? TipoId { get; set; }
    public PersonaNaturalDto? PersonaNatural { get; set; }
    public PersonaJuridicaDto? PersonaJuridica { get; set; }

}

public class PersonaNaturalTenantDto
{
    public string? NombreCompleto { get; set; }
}

public class PersonaJuridicaTenantDto
{
    public string? RazonSocial { get; set; }
}