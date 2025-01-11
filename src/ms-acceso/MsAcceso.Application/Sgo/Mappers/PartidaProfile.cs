using AutoMapper;
using MsAcceso.Domain.Tenant.PartidasTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class PartidaProfile : Profile
{
    public PartidaProfile()
    {
        CreateMap<PartidaTenant,PartidaTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        ;
    }
}