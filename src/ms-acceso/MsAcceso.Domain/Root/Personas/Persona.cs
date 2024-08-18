
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Domain.Root.Personas;

public sealed class Persona : Entity<PersonaId>
{
    private Persona(){}

    private Persona(
        PersonaId id,
        ParametroId tipo,
        ParametroId tipoDocumento,
        string numeroDocumento,
        string razonSocial
        ): base( id )
    {
        TipoId = tipo;
        TipoDocumentoId = tipoDocumento;
        NumeroDocumento = numeroDocumento;
        RazonSocial = razonSocial;
    }

    public ParametroId? TipoId {get; private set;}
    
    public ParametroId? TipoDocumentoId {get; private set;}

    public Parametro? Tipo {get; private set;}
    
    public Parametro? TipoDocumento {get; private set;}
    public string? NumeroDocumento {get; private set;}
    public string? RazonSocial {get; private set;}

}