
using AutoMapper;
using MsAcceso.Domain.Root.Especialidades;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class EspecialidadProfile : Profile
    {
        public EspecialidadProfile()
        {
            CreateMap<Especialidad, EspecialidadDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            ;
        }
    }
}