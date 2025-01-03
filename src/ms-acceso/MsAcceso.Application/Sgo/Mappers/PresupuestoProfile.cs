using AutoMapper;
using MsAcceso.Domain.Tenant.PresupuestosTenant;

namespace MsAcceso.Application.Sgo.Mappers;

public class PresupuestoProfile : Profile
{
    public PresupuestoProfile()
    {
        CreateMap<PresupuestoTenant,PresupuestoTenantDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id!.Value.ToString()))
        .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo))
        .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion))
        .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId!.Value.ToString()))
        .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoId))
        .ForMember(dest => dest.ProvinciaId, opt => opt.MapFrom(src => src.ProvinciaId))
        .ForMember(dest => dest.DistritoId, opt => opt.MapFrom(src => src.DistritoId))
        .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
        .ForMember(dest => dest.Plazodias, opt => opt.MapFrom(src => src.DistritoId))
        ;
    }
}