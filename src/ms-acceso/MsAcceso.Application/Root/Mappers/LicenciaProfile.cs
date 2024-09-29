
using AutoMapper;
using MsAcceso.Domain.Root.Licencias;

namespace MsAcceso.Application.Root.Mappers
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