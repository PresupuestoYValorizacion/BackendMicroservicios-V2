
using AutoMapper;
using MsAcceso.Domain.Root.DetalleProductos;

namespace MsAcceso.Application.Mappers
{
    public class DetalleProductoProfile : Profile
    {
        public DetalleProductoProfile()
        {
            CreateMap<DetalleProducto, DetalleProductoDto>()
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion!))
            .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion!));
        }
    }
}