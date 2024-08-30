using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Productos.DesactiveProductos;

internal sealed class DesactiveProductoCommandHandler : ICommandHandler<DesactiveProductoCommand, Guid>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IUnitOfWorkTenant _unitOfWork;

    public DesactiveProductoCommandHandler(
        IProductoRepository productoRepository,
        IUnitOfWorkTenant unitOfWork
    )
    {
        _productoRepository = productoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DesactiveProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await _productoRepository.GetByIdAsync(request.Id, cancellationToken);

        if(producto is null)
        {
            return Result.Failure<Guid>(ProductoErrors.NotFound);
        }

        producto.Desactive();

        _productoRepository.Update(producto);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(producto.Id!.Value, Message.Desactivate);
    }
}