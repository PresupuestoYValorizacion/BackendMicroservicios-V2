using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosPartidasRecursosTenant;

public sealed class PresupuestoEspecialidadTituloPartidaRecursoTenant : Entity<PresupuestoEspecialidadTituloPartidaRecursoTenantId>
{
    private PresupuestoEspecialidadTituloPartidaRecursoTenant(){}

    private PresupuestoEspecialidadTituloPartidaRecursoTenant(
        PresupuestoEspecialidadTituloPartidaRecursoTenantId id,
        PresupuestoEspecialidadTituloPartidaTenantId presupuestoEspecialidadTituloPartidaId,
        RecursoTenantId recursoId,
        int cantidad,
        int cuadrilla,
        double precio,
        double parcial

    ): base(id)
    {
        PresupuestoEspecialidadTituloPartidaId = presupuestoEspecialidadTituloPartidaId;
        RecursoId = recursoId;
        Cantidad = cantidad;
        Cuadrilla = cuadrilla;
        Precio = precio;
        Parcial = parcial;
    }

    public PresupuestoEspecialidadTituloPartidaTenantId? PresupuestoEspecialidadTituloPartidaId  {get; private set;}
    public PresupuestoEspecialidadTituloPartidaTenant? PresupuestoEspecialidadTituloPartida {get; private set;}
    public RecursoTenantId? RecursoId  {get; private set;}
    public RecursoTenant? Recurso  {get; private set;}
    public int? Cantidad {get; private set;}
    public int? Cuadrilla  {get; private set;}
    public double? Precio {get; private set;}
    public double? Parcial {get; private set;}

    public static PresupuestoEspecialidadTituloPartidaRecursoTenant Create(
        PresupuestoEspecialidadTituloPartidaRecursoTenantId Id,
        PresupuestoEspecialidadTituloPartidaTenantId PresupuestoEspecialidadTituloPartidaId,
        RecursoTenantId RecursoId,
        int Cantidad,
        int Cuadrilla,
        double Precio,
        double Parcial
    )
    {
        var presupuestoEspecialidadTituloPartidaRecurso = new PresupuestoEspecialidadTituloPartidaRecursoTenant(Id, PresupuestoEspecialidadTituloPartidaId, RecursoId, Cantidad, Cuadrilla, Precio, Parcial);
        return presupuestoEspecialidadTituloPartidaRecurso;
    }

    public Result Update(
        PresupuestoEspecialidadTituloPartidaTenantId presupuestoEspecialidadTituloPartidaId,
        RecursoTenantId recursoId,
        int cantidad,
        int cuadrilla,
        double precio,
        double parcial
    )
    {
        PresupuestoEspecialidadTituloPartidaId = presupuestoEspecialidadTituloPartidaId;
        RecursoId = recursoId;
        Cantidad = (cantidad > 0) ? cantidad : Cantidad;
        Cuadrilla = (cuadrilla > 0) ? cuadrilla : Cuadrilla;
        Precio = (precio > 0) ? precio : Precio;
        Parcial = (parcial > 0) ? parcial : Parcial;
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}
