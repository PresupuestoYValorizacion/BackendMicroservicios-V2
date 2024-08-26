
using AutoMapper;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Mappers
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            CreateMap<Rol, RolDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.TipoRolId, opt => opt.MapFrom(src => src.TipoRolId!.Value))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            ;

           
        }
    }
}