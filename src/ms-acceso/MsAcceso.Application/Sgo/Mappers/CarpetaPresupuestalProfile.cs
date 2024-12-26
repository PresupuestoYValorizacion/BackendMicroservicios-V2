using AutoMapper;
using MsAcceso.Domain.Tenant.CarpetasPresupuestalesTenant;
using MsAcceso.Domain.Tenant.ClientesTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class CarpetaPresupuestalProfile : Profile
{
    public CarpetaPresupuestalProfile()
    {
        CreateMap<CarpetaPresupuestalTenant,CarpetaPresupuestalTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember(dest => dest.Nivel, opt => opt.MapFrom(src => src.Nivel))
        .ForMember(dest => dest.Dependencia, opt => opt.MapFrom(src => src.Dependencia!.Value.ToString()))

        ;
    }
}