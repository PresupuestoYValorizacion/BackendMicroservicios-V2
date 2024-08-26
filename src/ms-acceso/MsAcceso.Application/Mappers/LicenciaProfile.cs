
using AutoMapper;
using MsAcceso.Domain.Root.Licencias;
using MsAcceso.Domain.Root.Rols;

namespace MsAcceso.Application.Mappers
{
    public class LicenciaProfile : Profile
    {
        public LicenciaProfile()
        {
            CreateMap<Licencia, LicenciaDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            ;

           
        }
    }
}