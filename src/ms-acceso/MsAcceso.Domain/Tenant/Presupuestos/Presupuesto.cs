
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Shared;


namespace MsAcceso.Domain.Tenant.Presupuestos;

public sealed class Presupuesto : Entity<PresupuestoId>
{
    private Presupuesto() {}

    private Presupuesto(
        PresupuestoId id,
        string nombre
        ): base(id)
    {
        Nombre = nombre;
    }

    public string? Nombre {get; private set;} 

    public static Presupuesto Create(
        string nombre
    )
    {
        var Presupuesto = new Presupuesto(PresupuestoId.New(), nombre);

        return Presupuesto;
    }

    public Result Update(string nombre)
    {
        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }


}
