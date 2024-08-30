using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Productos.DeleteProductos;

internal sealed class DeleteProductoCommandHandler : ICommandHandler<DeleteProductoCommand, Guid>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DeleteProductoCommandHandler(
        IProductoRepository productoRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _productoRepository = productoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await _productoRepository.GetByIdAsync(request.Id, cancellationToken);

        if (producto is null)
        {
            return Result.Failure<Guid>(ProductoErrors.NotFound);
        }

        _productoRepository.Delete(producto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(producto.Id!.Value, Message.Delete);
    }
}