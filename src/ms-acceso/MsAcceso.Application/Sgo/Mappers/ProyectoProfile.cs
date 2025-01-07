using AutoMapper;
using MsAcceso.Domain.Tenant.ProyectosTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class ProyectoProfile : Profile
{
    public ProyectoProfile()
    {
        CreateMap<ProyectoTenant,ProyectoTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.Especialidades))
        .ForMember(dest => dest.Presupuesto, opt => opt.MapFrom(src => src.Presupuesto))
        ;
    }
}