using AutoMapper;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTitulosTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class PresupuestoEspecialidadTituloProfile : Profile
{
    public PresupuestoEspecialidadTituloProfile()
    {
        CreateMap<PresupuestoEspecialidadTituloTenant,PresupuestoEspecialidadTituloTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo))
        .ForMember(dest => dest.Correlativo, opt => opt.MapFrom(src => src.Correlativo))
        ;
    }
}