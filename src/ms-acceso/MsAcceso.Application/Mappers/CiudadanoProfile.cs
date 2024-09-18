
using AutoMapper;
using MsAcceso.Domain.Root.Ciudadanos;

namespace MsAcceso.Application.Mappers
{
    public class CiudadanoProfile : Profile
    {
        public CiudadanoProfile()
        {
            CreateMap<Ciudadano, CiudadanoDto>()
            .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Apellido!))
            .ForMember(dest => dest.Nacionalidad, opt => opt.MapFrom(src => src.Nacionalidad!));
        }
    }
}