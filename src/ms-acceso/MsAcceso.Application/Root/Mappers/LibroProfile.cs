
using AutoMapper;
using MsAcceso.Domain.Root.Libros;

namespace MsAcceso.Application.Root.Mappers
{
    public class LibroProfile : Profile
    {
        public LibroProfile()
        {
            CreateMap<Libro, LibroDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion!))
            .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => src.Precio!))
            ;
        }
    }
}