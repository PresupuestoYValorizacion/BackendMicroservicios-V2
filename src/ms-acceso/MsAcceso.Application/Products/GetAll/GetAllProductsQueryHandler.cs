using System.IO.Compression;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Entity;
using MsAcceso.Domain.Repository;

namespace MsAcceso.Application.Opciones.GetOpcionByPagination;

internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, Product?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(
        IMapper mapper,
        IProductRepository productRepository
    )
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<Result<Product?>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {   
        return await _productRepository.GetByIdAsync(new ProductId(request.Id));
    }
}