using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Productos.RegisterProductos;

internal class RegisterProductoCommandHandler : ICommandHandler<RegisterProductoCommand, Guid>
{

    private readonly IProductoRepository _productoRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public RegisterProductoCommandHandler(
        IProductoRepository productoRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _productoRepository = productoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterProductoCommand request, CancellationToken cancellationToken)
    {
        var productoExists = await _productoRepository.ProductoExists(request.Nombre, cancellationToken);

        if(productoExists){
            return Result.Failure<Guid>(ProductoErrors.ProductoExists);
        }

        var Codigo = await _productoRepository.GetLastCodigoAsync(cancellationToken);;
        int lastCodigo = int.Parse(Codigo) + 1;
        Codigo = lastCodigo + "";
        
        var producto = Producto.Create(
            request.Nombre,
            Codigo,
            request.Cantidad
        );

        _productoRepository.Add(producto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(producto.Id!.Value, Message.Create);
    }
}