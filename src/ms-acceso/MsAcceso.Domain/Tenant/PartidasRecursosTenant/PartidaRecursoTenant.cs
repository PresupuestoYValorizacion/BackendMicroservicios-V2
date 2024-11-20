using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;
using MsAcceso.Domain.Tenant.PartidasTenant;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Domain.Tenant.PartidasRecursosTenant;

public sealed class PartidaRecursoTenant : Entity<PartidaRecursoTenantId>
{
    private PartidaRecursoTenant(){}

    private PartidaRecursoTenant(
        PartidaRecursoTenantId id,
        PartidaTenantId partidaId,
        RecursoTenantId recursoId,
        int cantidad,
        int cuadrilla,
        double precio,
        double parcial

    ): base(id)
    {
        PartidaId = partidaId;
        RecursoId = recursoId;
        Cantidad = cantidad;
        Cuadrilla = cuadrilla;
        Precio = precio;
        Parcial = parcial;
    }

    public PartidaTenantId? PartidaId  {get; private set;}
    public RecursoTenantId? RecursoId  {get; private set;}
    public PartidaTenant? Partida {get; private set;}
    public RecursoTenant? Recurso  {get; private set;}
    public int? Cantidad {get; private set;}
    public int? Cuadrilla  {get; private set;}
    public double? Precio {get; private set;}
    public double? Parcial {get; private set;}


    public static PartidaRecursoTenant Create(
        PartidaRecursoTenantId Id,
        PartidaTenantId PartidaId,
        RecursoTenantId RecursoId,
        int Cantidad,
        int Cuadrilla,
        double Precio,
        double Parcial
    )
    {
        var partidaRecurso = new PartidaRecursoTenant(Id, PartidaId, RecursoId, Cantidad, Cuadrilla, Precio, Parcial);
        return partidaRecurso;
    }

    public Result Update(
        PartidaTenantId partidaId,
        RecursoTenantId recursoId,
        int cantidad,
        int cuadrilla,
        double precio,
        double parcial
    )
    {
        PartidaId = partidaId;
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
