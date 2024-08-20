using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Personas;
using MsAcceso.Domain.Root.Sistemas;

namespace MsAcceso.Domain.Root.EmpresasSistemas;

public sealed class EmpresaSistema : Entity<EmpresaSistemaId>
{
    private EmpresaSistema(){}

    private EmpresaSistema(
        EmpresaSistemaId id,
        PersonaId empresaId,
        SistemaId sistemaId
        ): base( id )
    {
        EmpresaId = empresaId;
        SistemaId = sistemaId;
    }

    public PersonaId? EmpresaId {get; private set;}
    public SistemaId? SistemaId {get; private set;}


}

