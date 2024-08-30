using AutoMapper;
using MsAcceso.Application.Abstractions.Messaging;
using MsAcceso.Domain.Abstractions;
using MsAcceso.Domain.Root.Productos;


namespace MsAcceso.Application.Productos.GetAllProductos;


internal sealed class GetAllProductosQueryHandler : IQueryHandler<GetAllProductosQuery, List<ProductoDto>>
{
    private readonly IProductoRepository _productoRepository;

    private readonly IMapper _mapper;

    public GetAllProductosQueryHandler(
        IProductoRepository productoRepository,
        IMapper mapper
    )
    {
        _productoRepository = productoRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductoDto>>> Handle(GetAllProductosQuery request, CancellationToken cancellationToken)
    {
        var productos = await _productoRepository.GetAll();

        var productosDto = _mapper.Map<List<ProductoDto>>(productos);

        return productosDto!;

    }
}