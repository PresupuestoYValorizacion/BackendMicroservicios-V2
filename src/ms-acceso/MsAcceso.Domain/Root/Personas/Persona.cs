using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;
using MsAcceso.Domain.Root.PersonasJuridicas;
using MsAcceso.Domain.Root.PersonasNaturales;

namespace MsAcceso.Domain.Root.Personas;

public sealed class Persona : Entity<PersonaId>
{
    private Persona(){}

    private Persona(
        PersonaId id,
        ParametroId tipo,
        ParametroId tipoDocumento,
        string numeroDocumento
        ): base( id )
    {
        TipoId = tipo;
        TipoDocumentoId = tipoDocumento;
        NumeroDocumento = numeroDocumento;
    }

    public ParametroId? TipoId {get; private set;}
    
    public ParametroId? TipoDocumentoId {get; private set;}

    public Parametro? Tipo {get; private set;}
    
    public Parametro? TipoDocumento {get; private set;}
    public string? NumeroDocumento {get; private set;}
    public PersonaNatural? PersonaNatural {get; private set;}    
    public PersonaJuridica? PersonaJuridica {get; private set;}    

}