using AutoMapper;
using MsAcceso.Domain.Tenant.RecursosTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class RecursoProfile : Profile
{
    public RecursoProfile()
    {
        CreateMap<RecursoTenant,RecursoTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
        .ForMember(dest => dest.UnidadMedidaId, opt => opt.MapFrom(src => src.UnidadMedidaId))
        ;
    }
}