
using AutoMapper;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class EspecialidadTenantProfile : Profile
    {
        public EspecialidadTenantProfile()
        {
            CreateMap<EspecialidadTenant, EspecialidadTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            ;
        }
    }
}