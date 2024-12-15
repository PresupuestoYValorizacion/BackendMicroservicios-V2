using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasRecursosTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Domain.Tenant.PartidasTenant;

public sealed class PartidaTenant : Entity<PartidaTenantId>
{
    private PartidaTenant(){}

    private PartidaTenant(
        PartidaTenantId id,
        PartidaTenantId? dependencia,
        string nombre,
        int nivel
    ): base(id)
    {
        Dependencia = dependencia;
        Nombre = nombre;
        Nivel = nivel;
    }

    public PartidaTenantId? Dependencia {get; private set;}
    public PartidaTenant? DependenciaModel {get; private set;}
    public string? Nombre {get; private set;}
    public int? Nivel { get; private set; }
    public List<PartidaTenant>? Partidas { get; set; }
    public List<RecursoTenant>? Recursos { get; } = [];
    public List<PartidaRecursoTenant>? PartidasRecursos { get; } = [];

    public List<PresupuestoEspecialidadTituloTenant>? PresupuestosEspecialidadesTitulos { get; } = [];
    public List<PresupuestoEspecialidadTituloPartidaTenant> PresupuestosEspecialidadesTitulosPartidas { get; } = [];



    public static PartidaTenant Create(
        PartidaTenantId? dependencia, 
        string nombre, 
        int nivel
    )
    {
        var partida = new PartidaTenant(PartidaTenantId.New(), dependencia, nombre, nivel);
        return partida;
    }

    public Result Update(
        string nombre
    )
    {
        Nombre = (nombre.Length > 0 ) ? nombre : Nombre;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}
