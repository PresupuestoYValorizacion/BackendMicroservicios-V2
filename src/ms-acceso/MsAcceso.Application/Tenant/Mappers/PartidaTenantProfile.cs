using AutoMapper;
using MsAcceso.Domain.Tenant.PartidasTenant;


namespace MsAcceso.Application.Tenant.Mappers
{
    public class PartidaTenantProfile : Profile
    {
        public PartidaTenantProfile()
        {
            CreateMap<PartidaTenant, PartidaTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            .ForMember(dest => dest.Dependencia, opt => opt.MapFrom(src => src.Dependencia!.Value.ToString()))
            .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel))
            ;
        }
    }
}