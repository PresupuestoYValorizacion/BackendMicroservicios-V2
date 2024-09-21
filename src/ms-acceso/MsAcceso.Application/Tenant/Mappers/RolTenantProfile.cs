
using AutoMapper;
using MsAcceso.Domain.Root.Rols;
using MsAcceso.Domain.Tenant.RolsTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class RolTenantProfile : Profile
    {
        public RolTenantProfile()
        {
            CreateMap<RolTenant, RolTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!));
            
        }
    }
}