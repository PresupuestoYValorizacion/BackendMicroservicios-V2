using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Parametros;

namespace MsAcceso.Domain.Root.Sistemas;

public sealed class Sistema : Entity<SistemaId>
{
    private Sistema(){}

    public SistemaId? Dependencia {get; private set;}
    public string? Nombre {get; private set;}
    public string? Logo {get; private set;}
    public int? Nivel {get; private set;}
    public string? Url {get; private set;}
    public ParametroId? Tipo {get; private set;}
}
