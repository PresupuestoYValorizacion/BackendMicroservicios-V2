using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;

public sealed class PresupuestoEspecialidadTituloPartidaTenant : Entity<PresupuestoEspecialidadTituloPartidaTenantId>
{
    private PresupuestoEspecialidadTituloPartidaTenant(){}

    private PresupuestoEspecialidadTituloPartidaTenant(
        PresupuestoEspecialidadTituloPartidaTenantId id,
        PresupuestoEspecialidadTituloTenantId presupuestoEspecialidadTituloId,
        PartidaTenantId partidaId,
        string correlativo
    ) : base(id)
    {
        PresupuestoEspecialidadTituloId = presupuestoEspecialidadTituloId;
        PartidaId = partidaId;
        Correlativo = correlativo;
    }

    public PresupuestoEspecialidadTituloTenantId? PresupuestoEspecialidadTituloId {get; private set;}
    public PresupuestoEspecialidadTituloTenant? PresupuestoEspecialidadTitulo {get; private set;}
    public PartidaTenantId? PartidaId {get; private set;}
    public PartidaTenant? Partida {get; private set;}
    public string? Correlativo { get; private set; }

    public static PresupuestoEspecialidadTituloPartidaTenant Create(
        PresupuestoEspecialidadTituloPartidaTenantId Id,
        PresupuestoEspecialidadTituloTenantId PresupuestoEspecialidadTituloId,
        PartidaTenantId PartidaId,
        string Correlativo  
    )
    {
        var presupuestoEspecialidadTituloPartida = new PresupuestoEspecialidadTituloPartidaTenant(Id, PresupuestoEspecialidadTituloId, PartidaId, Correlativo);
        return presupuestoEspecialidadTituloPartida;
    }

    public Result Update(
        PresupuestoEspecialidadTituloTenantId presupuestoEspecialidadTituloId,
        PartidaTenantId partidaId,
        string correlativo
    )
    {
        PresupuestoEspecialidadTituloId = presupuestoEspecialidadTituloId;
        PartidaId = partidaId;
        Correlativo = (correlativo.Length > 0) ? correlativo : Correlativo;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    } 
}