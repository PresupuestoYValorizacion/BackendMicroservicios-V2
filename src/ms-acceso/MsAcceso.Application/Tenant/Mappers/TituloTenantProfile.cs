
using AutoMapper;
using MsAcceso.Domain.Tenant.TitulosTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class TituloTenantProfile : Profile
    {
        public TituloTenantProfile()
        {
            CreateMap<TituloTenant, TituloTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            ;
        }
    }
}