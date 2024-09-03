
using AutoMapper;
using MsAcceso.Domain.Root.ProductoProductoCategorias;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Mappers
{
    public class ProductoCategoriaProfile : Profile
    {
        public ProductoCategoriaProfile()
        {
            CreateMap<ProductoCategoria, ProductoCategoriaDto>();
        }
    }
}