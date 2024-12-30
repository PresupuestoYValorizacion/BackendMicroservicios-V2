using AutoMapper;
using MsAcceso.Domain.Tenant.EspecialidadesTenant;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class EspecialidadProfile : Profile
{
    public EspecialidadProfile()
    {
        CreateMap<EspecialidadTenant,EspecialidadTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        ;
    }
}