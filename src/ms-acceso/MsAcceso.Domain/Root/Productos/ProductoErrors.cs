

using MsAcceso.Domain.Abstractions;

namespace MsAcceso.Domain.Root.Productos;


public static class ProductoErrors
{
    public static Error ProductoExists = new(
        400,
        "Este producto ya existe, cree una nuevo"
    );
    public static Error ProductoCodigoExists = new(
        400,
        "Este codigo ya existe, cree una nuevo"
    );
    public static Error NotFound = new(
        404,
        "No existe el producto buscado"
    );


}