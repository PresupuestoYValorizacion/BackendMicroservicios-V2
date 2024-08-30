using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;
using MsAcceso.Domain.Shared;

namespace MsAcceso.Application.Productos.UpdateProductos;

internal class UpdateProductoCommandHandler : ICommandHandler<UpdateProductoCommand, Guid>
{

    private readonly IProductoRepository _productoRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public UpdateProductoCommandHandler(
        IProductoRepository productoRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _productoRepository = productoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
    {
        var producto =  await _productoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (producto is null)
        {
            return Result.Failure<Guid>(ProductoErrors.NotFound);
        }
        
        var productoExists = await _productoRepository.ProductoExists(request.Nombre!,cancellationToken);

        if(producto.Nombre != request.Nombre && productoExists){
            return Result.Failure<Guid>(ProductoErrors.ProductoExists);
        }

        producto.Update(
            request.Nombre,
            request.Codigo,
            request.Cantidad
        ); 


        _productoRepository.Update(producto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(producto.Id!.Value, Message.Update);
    }
} 