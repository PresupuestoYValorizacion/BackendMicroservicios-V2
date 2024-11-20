
using AutoMapper;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Tenant.Mappers
{
    public class RecursoTenantProfile : Profile
    {
        public RecursoTenantProfile()
        {
            CreateMap<RecursoTenant, RecursoTenantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre!))
            ;
        }
    }
}