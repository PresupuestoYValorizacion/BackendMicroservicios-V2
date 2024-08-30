
using AutoMapper;
using MsAcceso.Domain.Root.Productos;

namespace MsAcceso.Application.Mappers
{
    public class ProductoProfile : Profile
    {
        public ProductoProfile()
        {
            CreateMap<Producto, ProductoDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo!))
            .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad!));
        }
    }
}