using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Domain.Root.Resenias;

public sealed class Resenia : Entity<ReseniaId>
{
    private Resenia(){}

    private Resenia(
        ReseniaId id,
        ProductoId productoId,
        string comentario,
        int calificacion
    ): base(id)
    {
        Comentario = comentario;
        Calificacion = calificacion;
        ProductoId = productoId;
    }
    
    public ProductoId? ProductoId {get; private set; }
    public string? Comentario {get; private set; }
    public int? Calificacion {get; private set; }

    public static Resenia Create(
        ProductoId productoId,
        string comentario,
        int calificacion
    )
    {
        var detalleResenia = new Resenia(ReseniaId.New(), productoId, comentario, calificacion);

        return detalleResenia;
    }

    public Result Update(
        string comentario,
        int calificacion
    )
    {
        Comentario = (comentario.Length > 0) ? comentario : Comentario;
        Calificacion = (calificacion > 0) ? calificacion : Calificacion;

        return Result.Success();
    }

    public Result Desactive()
    {
        Activo = new Activo(false);
        return Result.Success();
    }
}