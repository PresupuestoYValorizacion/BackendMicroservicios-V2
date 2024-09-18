

using MsAcceso.Domain.Root.Pasaportes;

namespace MsAcceso.Domain.Root.Ciudadanos;
public class CiudadanoDto
{
    public string? Apellido { get; set; }   
    public string? Nacionalidad { get; set; }   
    public PasaporteDto? Pasaporte { get; set; }

}