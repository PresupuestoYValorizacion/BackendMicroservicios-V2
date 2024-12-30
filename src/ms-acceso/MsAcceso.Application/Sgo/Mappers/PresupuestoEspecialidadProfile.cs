using AutoMapper;
using MsAcceso.Domain.Tenant.PresupuestosEspecialidadTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class PresupuestoEspecialidadProfile : Profile
{
    public PresupuestoEspecialidadProfile()
    {
        CreateMap<PresupuestoEspecialidadTenant,PresupuestoEspecialidadTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        // .ForMember(dest => dest.ProyectoId, opt => opt.MapFrom(src => src.ProyectoId!.Value.ToString()))
        .ForMember(dest => dest.EspecialidadId, opt => opt.MapFrom(src => src.EspecialidadId!.Value.ToString()))
        ;
    }
}