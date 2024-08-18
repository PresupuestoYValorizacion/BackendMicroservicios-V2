using MediatR;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Repository;
using MsAcceso.Domain.Shared;


namespace MsAcceso.Application.Products.Create;


internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand , string>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWorkApplication _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWorkApplication unitOfWork

    )
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        
    }
    
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var product = new Product{
            Id = new ProductId(4),
            Name = request.Name,
            Supplier = request.Supplier,
            TenantId = "dante",
            Activo = new Activo(true)
        };

        _productRepository.Add(product);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        
        return Result.Success("Se registro correctamente", Message.Create)!;
    }
}